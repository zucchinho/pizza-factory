using System.Threading;
using System.Threading.Tasks;
using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.DataAccess.Model
{
    public interface IOrderReader
    {
        Task<IOrder> GetOrderAsync(string orderNumber);
        Task<IOrder> GetOrderAsync(string orderNumber, CancellationToken cancellationToken);
        IOrder GetOrder(string orderNumber);
    }
}