using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.Kitchen
{
    using NLog;
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SingleOvenKitchen : PizzaKitchen 
    {
        private readonly IPizzaOven _pizzaOven;

        public SingleOvenKitchen(
            IPizzaOven pizzaOven,
            ILogger logger = null) : base(logger)
        {
            this._pizzaOven = pizzaOven;
        }

        protected override async Task ProcessAsyncImpl(
            IPizzaOrder pizzaOrder, 
            double cookingTimeMs,
            CancellationToken cancellationToken)
        {
            // Cook the pizza (in the oven)
            await this._pizzaOven.CookAsync(
                pizzaOrder,
                cookingTimeMs,
                cancellationToken);
        }
    }
}
