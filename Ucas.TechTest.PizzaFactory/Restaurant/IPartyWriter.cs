using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ucas.TechTest.PizzaFactory.Model;

namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    public interface IPartyWriter
    {
        Task<IParty> CreatePartyAsync(int tableNumber, int partySize);
        Task<IParty> CreatePartyAsync(int tableNumber, int partySize, CancellationToken cancellationToken);
        IParty CreateParty(int tableNumber, int partySize);
    }
}