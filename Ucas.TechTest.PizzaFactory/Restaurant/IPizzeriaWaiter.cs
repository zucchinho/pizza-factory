using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.Restaurant
{
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
