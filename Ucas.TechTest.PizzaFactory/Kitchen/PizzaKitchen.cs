using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.Kitchen
{
    using NLog;
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

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

        private readonly IPizzaOven _pizzaOven;
        private readonly ILogger _logger;

        public PizzaKitchen(
            IPizzaOven pizzaOven)
            : this(pizzaOven, null)
        {
        }

        public PizzaKitchen(
            IPizzaOven pizzaOven,
            ILogger logger)
        {
            this._pizzaOven = pizzaOven ?? throw new ArgumentNullException(nameof(pizzaOven));
            this._logger = logger ?? LogManager.CreateNullLogger();
        }

        public async Task ProcessOrderAsync(
            IPizzaOrder pizzaOrder, 
            CancellationToken cancellationToken)
        {
            // Calculate cooking time
            var cookingTimeMs = pizzaOrder.Multiplier * BaseTimeMsLazy.Value;
            var toppingMultiplier = Convert.ToDouble(
                pizzaOrder.Topping.Count(c => char.IsLetter(c)));

            cookingTimeMs += toppingMultiplier * TimePerToppingLetterMsLazy.Value;

            this._logger.Debug(
                "Calculated total cooking time for pizza: {0}ms",
                cookingTimeMs);

            // Cook the pizza (in the oven)
            await this._pizzaOven.CookAsync(
                pizzaOrder,
                cookingTimeMs,
                cancellationToken);
        }
    }
}
