namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using System;
    using Ucas.TechTest.PizzaFactory.Model;

    public class RpgWaiter : IPizzeriaWaiter
    {
        private readonly IPizzaMenu pizzaMenu;
        private readonly Random rnd;

        public RpgWaiter(
            IPizzaMenu pizzaMenu)
        {
            this.pizzaMenu = pizzaMenu;
            this.rnd = new Random();
        }

        public IPizzaOrder GetNextOrder()
        {
            // Get random base
            var b = rnd.Next(
                this.pizzaMenu.PizzaBases.Count);
            var pizzaBase = this.pizzaMenu.PizzaBases[b];

            // Get random topping
            var t = rnd.Next(
                this.pizzaMenu.Toppings.Count);
            var topping = this.pizzaMenu.Toppings[t];

            return new PizzaOrder(
                pizzaBase,
                topping);
        }
    }
}
