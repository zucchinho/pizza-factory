using System.Collections.Generic;

namespace Ucas.TechTest.PizzaFactory.Core.Model
{
    /// <summary>
    /// Represents a pizza menu comprised of available bases and toppings
    /// </summary>
    public interface IPizzaMenu
    {
        /// <summary>
        /// Gets the pizza bases.
        /// </summary>
        /// <value>
        /// The pizza bases.
        /// </value>
        IReadOnlyList<IPizzaBase> PizzaBases { get; }

        /// <summary>
        /// Gets the toppings.
        /// </summary>
        /// <value>
        /// The toppings.
        /// </value>
        IReadOnlyList<string> Toppings { get; }
    }
}
