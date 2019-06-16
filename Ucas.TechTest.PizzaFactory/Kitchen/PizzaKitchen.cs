namespace Ucas.TechTest.PizzaFactory.Kitchen
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Ucas.TechTest.PizzaFactory.Model;

    public class PizzaKitchen : IPizzaKitchen
    {
        private static readonly Lazy<double> BaseTimeMsLazy = new Lazy<double>(
            () =>
            {
                return Convert.ToDouble(ConfigurationManager.AppSettings["PizzaKitchen.BaseTimeMilliseconds"]);
            });

        private static readonly Lazy<double> TimePerToppingLetterMsLazy = new Lazy<double>(
            () =>
            {
                return Convert.ToDouble(ConfigurationManager.AppSettings["PizzaKitchen.TimePerToppingLetterMilliseconds"]);
            });

        private readonly IPizzaOven pizzaOven;

        public PizzaKitchen(IPizzaOven pizzaOven)
        {
            this.pizzaOven = pizzaOven;
        }

        public async Task ProcessOrderAsync(
            IPizzaOrder pizzaOrder, 
            CancellationToken cancellationToken)
        {
            // Calculate cooking time
            var cookingTimeMs = pizzaOrder.Multiplier * BaseTimeMsLazy.Value;
            var toppingMultiplier = Convert.ToDouble(
                pizzaOrder.Topping.Count(c => char.IsLetter(c)));

            cookingTimeMs += (toppingMultiplier * TimePerToppingLetterMsLazy.Value);

            // Cook the pizza
            await this.pizzaOven.CookAsync(
                pizzaOrder,
                cookingTimeMs,
                cancellationToken);
        }
    }
}
