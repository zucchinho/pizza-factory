namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using System;
    using System.Configuration;
    using System.Threading;
    using System.Threading.Tasks;
    using Ucas.TechTest.PizzaFactory.Kitchen;

    public class Pizzeria : IPizzeria
    {
        private readonly IPizzeriaWaiter waiter;
        private readonly IPizzaKitchen pizzaKitchen;

        private static readonly Lazy<double> DefaultCookingIntervalMsLazy = new Lazy<double>(
            () =>
            {
                return Convert.ToDouble(ConfigurationManager.AppSettings["Pizzeria.DefaultCookingIntervalMilliseconds"]);
            });

        public Pizzeria(
            IPizzeriaWaiter waiter, 
            IPizzaKitchen pizzaKitchen)
        {
            this.waiter = waiter;
            this.pizzaKitchen = pizzaKitchen;

            // Add a simple delegate to return the default interval (from the config)
            this.OrderInterval += () => DefaultCookingIntervalMsLazy.Value;
        }

        /// <summary>
        /// Occurs when [order interval].
        /// </summary>
        public event Func<double> OrderInterval;

        public async Task CaterAsync(
            int partySize, 
            CancellationToken cancellationToken)
        {
            var pizzaCount = 0;

            while (pizzaCount < partySize)
            {
                var nextOrder = this.waiter.GetNextOrder();

                // Simulate the cooking interval
                await Task.Delay(
                    TimeSpan.FromMilliseconds(this.OrderInterval.Invoke()));

                // Wait for the kitchen to process the order
                await this.pizzaKitchen.ProcessOrderAsync(
                    nextOrder,
                    cancellationToken);

                // Increment counter
                ++pizzaCount;

                Console.WriteLine($"Processed order number: {pizzaCount}");
            }
        }
    }
}