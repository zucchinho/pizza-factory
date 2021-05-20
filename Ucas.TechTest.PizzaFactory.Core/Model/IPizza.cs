using System;

namespace Ucas.TechTest.PizzaFactory.Core.Model
{
    /// <summary>
    /// Represents a pizza
    /// </summary>
    /// <seealso cref="IPizzaOrder" />
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
