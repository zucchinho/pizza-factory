using System.Threading;
using System.Threading.Tasks;
using Ucas.TechTest.PizzaFactory.Model;

namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    public interface IMenuReader
    {
        Task<IPizzaMenu> GetMenuAsync();
        Task<IPizzaMenu> GetMenuAsync(CancellationToken cancellationToken);
        IPizzaMenu GetMenu();
    }
}