﻿namespace Ucas.TechTest.PizzaFactory.Model
{
    /// <summary>
    /// Models data relevant to a pizza order
    /// </summary>
    /// <seealso cref="Ucas.TechTest.PizzaFactory.Model.PizzaBase" />
    /// <seealso cref="Ucas.TechTest.PizzaFactory.Model.IPizzaOrder" />
    public class PizzaOrder : PizzaBase, IPizzaOrder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PizzaOrder"/> class.
        /// </summary>
        /// <param name="pizzaBaseName">Name of the pizza base.</param>
        /// <param name="baseMultiplier">The base multiplier.</param>
        /// <param name="topping">The topping.</param>
        public PizzaOrder(
            string pizzaBaseName,
            double baseMultiplier,
            string topping)
            : base(pizzaBaseName, baseMultiplier)
        {
            this.Topping = topping;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PizzaOrder"/> class.
        /// </summary>
        /// <param name="pizzaBase">The pizza base.</param>
        /// <param name="topping">The topping.</param>
        public PizzaOrder(
            IPizzaBase pizzaBase,
            string topping)
            :base(pizzaBase.Name, pizzaBase.Multiplier)
        {
            this.Topping = topping;
        }

        /// <summary>
        /// Gets the topping.
        /// </summary>
        /// <value>
        /// The topping.
        /// </value>
        public string Topping { get; }

        /// <summary>
        /// Gets the name of the base.
        /// </summary>
        /// <value>
        /// The name of the base.
        /// </value>
        public string BaseName => this.Name;
    }
}
