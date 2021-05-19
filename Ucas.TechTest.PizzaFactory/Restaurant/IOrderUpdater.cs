using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ucas.TechTest.PizzaFactory.Model;

namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    public interface IOrderUpdater
    {
        Task UpdateOrderAsync(OrderStatus orderStatus);
        Task UpdateOrderAsync(OrderStatus orderStatus, CancellationToken cancellationToken);
        void UpdateOrder(OrderStatus orderStatus);
    }
}