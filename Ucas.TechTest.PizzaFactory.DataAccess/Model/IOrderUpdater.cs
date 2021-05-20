using System.Threading;
using System.Threading.Tasks;
using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.DataAccess.Model
{
    public interface IOrderUpdater
    {
        Task<bool> UpdateOrderAsync(string orderNumber, OrderStatus orderStatus);
        Task<bool> UpdateOrderAsync(string orderNumber, OrderStatus orderStatus, CancellationToken cancellationToken);
        bool UpdateOrder(string orderNumber, OrderStatus orderStatus);
    }
}