namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using System.Collections.Generic;
    using Ucas.TechTest.PizzaFactory.Model;

    /// <summary>
    /// Dummy implementation of a pizza menu (with hard-coded options for bases, toppings)
    /// </summary>
    /// <seealso cref="Ucas.TechTest.PizzaFactory.Model.IPizzaMenu" />
    public class PizzaMenu : IPizzaMenu
    {
        /// <summary>
        /// Gets the pizza bases.
        /// </summary>
        /// <value>
        /// The pizza bases.
        /// </value>
        public IReadOnlyList<IPizzaBase> PizzaBases { get; set; }

        /// <summary>
        /// Gets the toppings.
        /// </summary>
        /// <value>
        /// The toppings.
        /// </value>
        public IReadOnlyList<string> Toppings { get; set; }
    }
}
