using System.Threading;
using System.Threading.Tasks;
using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.DataAccess.Model
{
    public interface IMenuReader
    {
        Task<IPizzaMenu> GetMenuAsync();
        Task<IPizzaMenu> GetMenuAsync(CancellationToken cancellationToken);
        IPizzaMenu GetMenu();
    }
}