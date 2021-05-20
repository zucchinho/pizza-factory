using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using System.Collections.Generic;

    /// <summary>
    /// Dummy implementation of a pizza menu (with hard-coded options for bases, toppings)
    /// </summary>
    /// <seealso cref="IPizzaMenu" />
    public class DummyPizzaMenu : IPizzaMenu
    {
        /// <summary>
        /// Gets the pizza bases.
        /// </summary>
        /// <value>
        /// The pizza bases.
        /// </value>
        public IReadOnlyList<IPizzaBase> PizzaBases { get; } = new List<IPizzaBase>
        {
            new PizzaBase( "Deep Pan", 2),
            new PizzaBase("Stuffed Crust", 1.5),
            new PizzaBase("Thin and Crispy", 2)
        };

        /// <summary>
        /// Gets the toppings.
        /// </summary>
        /// <value>
        /// The toppings.
        /// </value>
        public IReadOnlyList<string> Toppings { get; } = new List<string>
        {
            "Ham and Mushroom",
            "Pepperoni",
            "Vegetable"
        };
    }
}
