using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.Kitchen
{
    using NLog;
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class PizzaKitchen : IPizzaKitchen
    {
        private static readonly Lazy<double> BaseTimeMsLazy = new Lazy<double>(
            () => Convert.ToDouble(ConfigurationManager.AppSettings["SingleOvenKitchen.BaseTimeMilliseconds"]));

        private static readonly Lazy<double> TimePerToppingLetterMsLazy = new Lazy<double>(
            () => Convert.ToDouble(ConfigurationManager.AppSettings["SingleOvenKitchen.TimePerToppingLetterMilliseconds"]));

        private readonly ILogger _logger;

        protected PizzaKitchen()
            : this(null)
        {
        }

        protected PizzaKitchen(
            ILogger logger)
        {
            this._logger = logger ?? LogManager.CreateNullLogger();
        }

        public Task ProcessOrderAsync(
            IPizzaOrder pizzaOrder, 
            CancellationToken cancellationToken)
        {
            // Calculate cooking time
            var cookingTimeMs = pizzaOrder.Multiplier * BaseTimeMsLazy.Value;
            var toppingMultiplier = Convert.ToDouble(
                pizzaOrder.Topping.Count(char.IsLetter));

            cookingTimeMs += toppingMultiplier * TimePerToppingLetterMsLazy.Value;

            this._logger.Debug(
                "Calculated total cooking time for pizza: {0}ms",
                cookingTimeMs);

            return this.ProcessAsyncImpl(pizzaOrder, cookingTimeMs, cancellationToken);
        }

        protected abstract Task ProcessAsyncImpl(
            IPizzaOrder pizzaOrder,
            double cookingTimeMs,
            CancellationToken cancellationToken);
    }
}
