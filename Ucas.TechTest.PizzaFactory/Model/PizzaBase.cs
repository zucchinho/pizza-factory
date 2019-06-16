namespace Ucas.TechTest.PizzaFactory.Model
{
    /// <summary>
    /// Models data relevant to a pizza base
    /// </summary>
    /// <seealso cref="Ucas.TechTest.PizzaFactory.Model.IPizzaBase" />
    public class PizzaBase : IPizzaBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PizzaBase"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="multiplier">The multiplier.</param>
        public PizzaBase(
            string name, 
            double multiplier)
        {
            this.Name = name;
            this.Multiplier = multiplier;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the multiplier.
        /// </summary>
        /// <value>
        /// The multiplier.
        /// </value>
        public double Multiplier { get; }
    }
}