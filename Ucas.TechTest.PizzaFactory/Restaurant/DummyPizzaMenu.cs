namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using System.Collections.Generic;
    using Ucas.TechTest.PizzaFactory.Model;

    public class DummyPizzaMenu : IPizzaMenu
    {
        public IReadOnlyList<IPizzaBase> PizzaBases { get; } = new List<IPizzaBase>
        {
            new PizzaBase( "Deep Pan", 2),
            new PizzaBase("Stuffed Crust", 1.5),
            new PizzaBase("Thin and Crispy", 2)
        };

        public IReadOnlyList<string> Toppings { get; } = new List<string>
        {
            "Ham and Mushroom",
            "Pepperoni",
            "Vegetable"
        };
    }
}
