namespace Ucas.TechTest.PizzaFactory.Kitchen
{
    using System.Threading;
    using System.Threading.Tasks;
    using Ucas.TechTest.PizzaFactory.Model;

    /// <summary>
    /// Provides method signatures to cook pizzas
    /// </summary>
    public interface IPizzaOven
    {
        /// <summary>
        /// Cooks the asynchronous.
        /// </summary>
        /// <param name="pizzaOrder">The pizza order.</param>
        /// <param name="cookingTimeMs">The cooking time ms.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task CookAsync(
            IPizzaOrder pizzaOrder,
            double cookingTimeMs,
            CancellationToken cancellationToken);
    }
}
