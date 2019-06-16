namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using NLog;
    using System;
    using Ucas.TechTest.PizzaFactory.Model;

    public class RpgWaiter : IPizzeriaWaiter
    {
        private static Random rnd;
        private readonly IPizzaMenu pizzaMenu;
        private readonly ILogger logger;

        public RpgWaiter(
            IPizzaMenu pizzaMenu)
            : this(pizzaMenu, null)
        {
        }

        public RpgWaiter(
            IPizzaMenu pizzaMenu, 
            ILogger logger)
        {
            this.pizzaMenu = pizzaMenu ?? throw new ArgumentNullException(nameof(pizzaMenu));
            this.logger = logger ?? LogManager.CreateNullLogger();

            rnd = new Random();
        }

        public IPizzaOrder GetNextOrder()
        {
            // Get random base
            var b = rnd.Next(
                this.pizzaMenu.PizzaBases.Count);
            var pizzaBase = this.pizzaMenu.PizzaBases[b];

            this.logger.Trace(
                "Generated random pizza base: {0}",
                pizzaBase.Name);

            // Get random topping
            var t = rnd.Next(
                this.pizzaMenu.Toppings.Count);
            var topping = this.pizzaMenu.Toppings[t];

            this.logger.Trace(
                "Generated random pizza topping",
                topping);

            return new PizzaOrder(
                pizzaBase,
                topping);
        }
    }
}
