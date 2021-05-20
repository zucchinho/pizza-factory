namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using NLog;
    using System;
    using System.Configuration;
    using System.Threading;
    using System.Threading.Tasks;
    using Ucas.TechTest.PizzaFactory.Kitchen;

    /// <summary>
    /// Generic implementation of a pizzeria, caters to pizza parties
    /// </summary>
    /// <seealso cref="Ucas.TechTest.PizzaFactory.Restaurant.IPizzeria" />
    public class Pizzeria : IPizzeria
    {
        /// <summary>
        /// The waiter
        /// </summary>
        private readonly IPizzeriaWaiter _waiter;
        /// <summary>
        /// The pizza kitchen
        /// </summary>
        private readonly IPizzaKitchen _pizzaKitchen;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The default cooking interval ms lazy
        /// </summary>
        private static readonly Lazy<double> DefaultCookingIntervalMsLazy = new Lazy<double>(
            () => Convert.ToDouble(ConfigurationManager.AppSettings["Pizzeria.DefaultOrderIntervalMilliseconds"]));

        /// <summary>
        /// Initializes a new instance of the <see cref="Pizzeria"/> class.
        /// </summary>
        /// <param name="waiter">The waiter.</param>
        /// <param name="pizzaKitchen">The pizza kitchen.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="System.ArgumentNullException">
        /// waiter
        /// or
        /// pizzaKitchen
        /// </exception>
        public Pizzeria(
            IPizzeriaWaiter waiter, 
            IPizzaKitchen pizzaKitchen, 
            ILogger logger = null)
        {
            this._waiter = waiter ?? throw new ArgumentNullException(nameof(waiter));
            this._pizzaKitchen = pizzaKitchen ?? throw new ArgumentNullException(nameof(pizzaKitchen));

            this._logger = logger ?? LogManager.CreateNullLogger();

            // Add a simple delegate to return the default interval (from the config)
            this.OrderInterval += () => DefaultCookingIntervalMsLazy.Value;
        }

        /// <summary>
        /// Occurs when the next order interval is required.
        /// </summary>
        public event Func<double> OrderInterval;

        /// <summary>
        /// Caters the asynchronous.
        /// </summary>
        /// <param name="partySize">Size of the party.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task CaterAsync(CancellationToken cancellationToken)
        {
            var orderNumber = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                this._logger.Info(
                    "Beginning to process new order: {0}",
                    orderNumber);

                try
                {
                    // Get the next order via the waiter
                    var nextOrder = this._waiter.GetNextOrder();

                    if (nextOrder != null)
                    {
                        // Wait for the kitchen to process the order
                        await this._pizzaKitchen.ProcessOrderAsync(
                            nextOrder,
                            cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    this._logger.Fatal(
                        ex,
                        "Something went wrong with order: {0}",
                        orderNumber);
                    throw;
                }

                this._logger.Info(
                    "Finished processing order: {0}",
                    orderNumber);

                // Simulate the cooking interval
                var nextIntervalMs = this.OrderInterval.Invoke();
                this._logger.Debug(
                    "Waiting for {0} milliseconds before processing next order.",
                    nextIntervalMs);

                await Task.Delay(
                    TimeSpan.FromMilliseconds(nextIntervalMs),
                    cancellationToken);
            }
        }
    }
}