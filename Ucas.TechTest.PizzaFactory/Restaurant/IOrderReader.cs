using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ucas.TechTest.PizzaFactory.Model;

namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    public interface IOrderReader
    {
        Task<IOrder> GetOrderAsync(string orderNumber);
        Task<IOrder> GetOrderAsync(string orderNumber, CancellationToken cancellationToken);
        IOrder GetOrder(string orderNumber);
    }
}