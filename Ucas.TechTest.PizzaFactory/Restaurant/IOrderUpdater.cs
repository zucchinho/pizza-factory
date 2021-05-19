using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ucas.TechTest.PizzaFactory.Model;

namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    public interface IOrderUpdater
    {
        Task<bool> UpdateOrderAsync(string orderNumber, OrderStatus orderStatus);
        Task<bool> UpdateOrderAsync(string orderNumber, OrderStatus orderStatus, CancellationToken cancellationToken);
        bool UpdateOrder(string orderNumber, OrderStatus orderStatus);
    }
}