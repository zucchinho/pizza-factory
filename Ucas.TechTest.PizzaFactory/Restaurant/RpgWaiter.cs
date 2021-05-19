﻿namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using NLog;
    using System;
    using Ucas.TechTest.PizzaFactory.Model;

    /// <summary>
    /// Dummy implementation of a pizzeria waiter (randomly generates pizza order combinations)
    /// </summary>
    /// <seealso cref="Ucas.TechTest.PizzaFactory.Restaurant.IPizzeriaWaiter" />
    public class RpgWaiter : IPizzeriaWaiter
    {
        /// <summary>
        /// The random
        /// </summary>
        private static Random _rnd;
        /// <summary>
        /// The pizza menu
        /// </summary>
        private readonly IPizzaMenu _pizzaMenu;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RpgWaiter"/> class.
        /// </summary>
        /// <param name="pizzaMenu">The pizza menu.</param>
        public RpgWaiter(
            IPizzaMenu pizzaMenu)
            : this(pizzaMenu, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpgWaiter"/> class.
        /// </summary>
        /// <param name="pizzaMenu">The pizza menu.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="System.ArgumentNullException">pizzaMenu</exception>
        public RpgWaiter(
            IPizzaMenu pizzaMenu, 
            ILogger logger)
        {
            this._pizzaMenu = pizzaMenu ?? throw new ArgumentNullException(nameof(pizzaMenu));
            this._logger = logger ?? LogManager.CreateNullLogger();

            _rnd = new Random();
        }

        /// <summary>
        /// Gets the next order.
        /// </summary>
        /// <returns></returns>
        public IPizzaOrder GetNextOrder()
        {
            // Get random base
            var b = _rnd.Next(
                this._pizzaMenu.PizzaBases.Count);
            var pizzaBase = this._pizzaMenu.PizzaBases[b];

            this._logger.Trace(
                "Generated random pizza base: {0}",
                pizzaBase.Name);

            // Get random topping
            var t = _rnd.Next(
                this._pizzaMenu.Toppings.Count);
            var topping = this._pizzaMenu.Toppings[t];

            this._logger.Trace(
                "Generated random pizza topping",
                topping);

            // Return the order
            return new PizzaOrder(
                pizzaBase,
                topping);
        }
    }
}
