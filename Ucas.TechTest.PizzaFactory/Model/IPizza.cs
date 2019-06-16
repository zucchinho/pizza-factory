namespace Ucas.TechTest.PizzaFactory.Model
{
    using System;

    /// <summary>
    /// Represents a pizza
    /// </summary>
    /// <seealso cref="Ucas.TechTest.PizzaFactory.Model.IPizzaOrder" />
    /// <seealso cref="System.IFormattable" />
    public interface IPizza : IPizzaOrder, IFormattable
    {
        /// <summary>
        /// Gets the cooking time.
        /// </summary>
        /// <value>
        /// The cooking time.
        /// </value>
        TimeSpan CookingTime { get; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        string ToString();

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        string ToString(string format);
    }
}
