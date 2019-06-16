namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using Ucas.TechTest.PizzaFactory.Model;

    /// <summary>
    /// Provides method signatures to generate pizza orders
    /// </summary>
    public interface IPizzeriaWaiter
    {
        /// <summary>
        /// Gets the next order.
        /// </summary>
        /// <returns></returns>
        IPizzaOrder GetNextOrder();
    }
}
