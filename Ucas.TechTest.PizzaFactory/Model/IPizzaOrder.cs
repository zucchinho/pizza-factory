namespace Ucas.TechTest.PizzaFactory.Model
{
    /// <summary>
    /// Represents a pizza order
    /// </summary>
    /// <seealso cref="Ucas.TechTest.PizzaFactory.Model.IPizzaBase" />
    public interface IPizzaOrder : IPizzaBase
    {
        /// <summary>
        /// Gets the topping.
        /// </summary>
        /// <value>
        /// The topping.
        /// </value>
        string Topping { get; }

        /// <summary>
        /// Gets the name of the base.
        /// </summary>
        /// <value>
        /// The name of the base.
        /// </value>
        string BaseName { get; }
    }
}
