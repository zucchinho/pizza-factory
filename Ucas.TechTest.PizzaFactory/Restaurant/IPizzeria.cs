namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides method signatures to cater for pizza parties
    /// </summary>
    public interface IPizzeria
    {
        /// <summary>
        /// Caters the asynchronous.
        /// </summary>
        /// <param name="partySize">Size of the party.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task CaterAsync(
            int partySize,
            CancellationToken cancellationToken);
    }
}
