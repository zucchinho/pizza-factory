namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using System.Threading;
    using System.Threading.Tasks;
    using Ucas.TechTest.PizzaFactory.Kitchen;

    public class Pizzeria : IPizzeria
    {
        private readonly IPizzeriaWaiter waiter;
        private readonly IPizzaKitchen pizzaKitchen;

        public async Task CaterAsync(int partySize, CancellationToken cancellationToken)
        {
            var pizzaCount = 0;

            while (pizzaCount < partySize)
            {
                var nextOrder = this.waiter.GetNextOrder();

                // Wait for the kitchen to process the order
                await this.pizzaKitchen.ProcessOrderAsync(
                    nextOrder,
                    cancellationToken);

                // Increment counter
                ++pizzaCount;
            }
        }
    }
}