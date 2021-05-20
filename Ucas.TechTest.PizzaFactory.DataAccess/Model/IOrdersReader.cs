using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.DataAccess.Model
{
    public interface IOrdersReader
    {
        Task<IEnumerable<IOrder>> GetOrdersAsync(OrderStatus orderStatus);
        Task<IEnumerable<IOrder>> GetOrdersAsync(OrderStatus orderStatus, CancellationToken cancellationToken);
        IEnumerable<IOrder> GetOrders(OrderStatus orderStatus);
        Task<IEnumerable<IOrder>> GetOrdersAsync();
        Task<IEnumerable<IOrder>> GetOrdersAsync(CancellationToken cancellationToken);
        IEnumerable<IOrder> GetOrders();
    }
}