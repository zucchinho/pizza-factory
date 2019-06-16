namespace Ucas.TechTest.PizzaFactory.Kitchen
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Ucas.TechTest.PizzaFactory.Model;

    /// <summary>
    /// Provides method signatures for processing pizza orders
    /// </summary>
    public interface IPizzaKitchen
    {
        /// <summary>
        /// Processes the order asynchronously.
        /// </summary>
        /// <param name="pizzaOrder">The pizza order.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A <see cref="Task" /> to process the order.
        /// </returns>
        Task ProcessOrderAsync(
            IPizzaOrder pizzaOrder,
            CancellationToken cancellationToken);

        /// <summary>
        /// Occurs when [cooking interval].
        /// </summary>
        event Func<double> CookingInterval;
    }
}
