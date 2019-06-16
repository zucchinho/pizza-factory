namespace Ucas.TechTest.PizzaFactory.Model
{
    using System;

    /// <summary>
    /// Models data relevant to a pizza
    /// </summary>
    /// <seealso cref="Ucas.TechTest.PizzaFactory.Model.PizzaOrder" />
    /// <seealso cref="Ucas.TechTest.PizzaFactory.Model.IPizza" />
    public class Pizza : PizzaOrder, IPizza
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pizza"/> class.
        /// </summary>
        /// <param name="pizzaOrder">The pizza order.</param>
        /// <param name="cookingTimeMs">The cooking time ms.</param>
        public Pizza(
            IPizzaOrder pizzaOrder,
            double cookingTimeMs)
            : base(pizzaOrder, pizzaOrder.Topping)
        {
            this.CookingTime = TimeSpan.FromMilliseconds(cookingTimeMs);
        }

        /// <summary>
        /// Gets the cooking time.
        /// </summary>
        /// <value>
        /// The cooking time.
        /// </value>
        public TimeSpan CookingTime { get; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public new string ToString()
        {
            return this.ToString("U");
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public string ToString(string format)
        {
            return this.ToString(format, null);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// <exception cref="System.FormatException">Unrecognized option: {format}. Use C for Cooked, or U for Uncooked.</exception>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch(format ?? "U")
            {
                case "C":
                    return $"{this.Name} w/ {this.Topping} cooked in {this.CookingTime.TotalMilliseconds}ms";
                case "U":
                    return $"{this.Name} w/ {this.Topping}";
                default:
                    throw new FormatException($"Unrecognized option: {format}. Use C for Cooked, or U for Uncooked.");
            }            
        }
    }
}
