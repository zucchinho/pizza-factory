using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ucas.TechTest.PizzaFactory.Model;

namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    public interface IOrderWriter
    {
        Task<IOrder> CreateOrderAsync(IEnumerable<IPizzaOrder> orderItems);
        Task<IOrder> CreateOrderAsync(IEnumerable<IPizzaOrder> orderItems, CancellationToken cancellationToken);
        IOrder CreateOrder(IEnumerable<IPizzaOrder> orderItems);
    }
}