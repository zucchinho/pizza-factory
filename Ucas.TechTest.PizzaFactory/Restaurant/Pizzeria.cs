namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using NLog;
    using System;
    using System.Configuration;
    using System.Threading;
    using System.Threading.Tasks;
    using Ucas.TechTest.PizzaFactory.Kitchen;

    public class Pizzeria : IPizzeria
    {
        private readonly IPizzeriaWaiter waiter;
        private readonly IPizzaKitchen pizzaKitchen;
        private readonly ILogger logger;

        private static readonly Lazy<double> DefaultCookingIntervalMsLazy = new Lazy<double>(
            () =>
            {
                return Convert.ToDouble(ConfigurationManager.AppSettings["Pizzeria.DefaultOrderIntervalMilliseconds"]);
            });

        public Pizzeria(
            IPizzeriaWaiter waiter, 
            IPizzaKitchen pizzaKitchen)
            : this(waiter, pizzaKitchen, null)
        {
        }

        public Pizzeria(
            IPizzeriaWaiter waiter, 
            IPizzaKitchen pizzaKitchen, 
            ILogger logger)
        {
            this.waiter = waiter ?? throw new ArgumentNullException(nameof(waiter));
            this.pizzaKitchen = pizzaKitchen ?? throw new ArgumentNullException(nameof(pizzaKitchen));

            this.logger = logger ?? LogManager.CreateNullLogger();

            // Add a simple delegate to return the default interval (from the config)
            this.OrderInterval += () => DefaultCookingIntervalMsLazy.Value;
        }

        /// <summary>
        /// Occurs when the next order interval is required.
        /// </summary>
        public event Func<double> OrderInterval;

        public async Task CaterAsync(
            int partySize, 
            CancellationToken cancellationToken)
        {
            var orderNumber = 0;

            while (orderNumber < partySize
                && !cancellationToken.IsCancellationRequested)
            {
                // Increment counter
                ++orderNumber;

                this.logger.Info(
                    "Beginning to process new order: {0}",
                    orderNumber);

                try
                {
                    // Get the next order via the waiter
                    var nextOrder = this.waiter.GetNextOrder();

                    // Wait for the kitchen to process the order
                    await this.pizzaKitchen.ProcessOrderAsync(
                        nextOrder,
                        cancellationToken);
                }
                catch (Exception ex)
                {
                    this.logger.Fatal(
                        ex,
                        "Something went wrong with order: {0}",
                        orderNumber);
                    throw;
                }

                this.logger.Info(
                    "Finished processing order: {0}",
                    orderNumber);

                if(orderNumber == partySize)
                {
                    this.logger.Info(
                        "That was the last order.");
                    continue;
                }

                // Simulate the cooking interval
                var nextIntervalMs = this.OrderInterval.Invoke();
                this.logger.Debug(
                    "Waiting for {0} milliseconds before processing next order.",
                    nextIntervalMs);

                await Task.Delay(
                    TimeSpan.FromMilliseconds(nextIntervalMs),
                    cancellationToken);
            }
        }
    }
}