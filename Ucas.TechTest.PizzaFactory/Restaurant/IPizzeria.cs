using System;
namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IPizzeria
    {
        Task CaterAsync(
            int partySize,
            CancellationToken cancellationToken);
    }
}
