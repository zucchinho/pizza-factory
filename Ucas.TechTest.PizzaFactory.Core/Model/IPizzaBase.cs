namespace Ucas.TechTest.PizzaFactory.Core.Model
{
    /// <summary>
    /// Represents a pizza base
    /// </summary>
    public interface IPizzaBase
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the multiplier.
        /// </summary>
        /// <value>
        /// The multiplier.
        /// </value>
        double Multiplier { get; }
    }
}