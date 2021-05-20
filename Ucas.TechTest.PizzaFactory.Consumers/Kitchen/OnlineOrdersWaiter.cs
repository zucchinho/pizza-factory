using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Ucas.TechTest.PizzaFactory.Core.Model;
using Ucas.TechTest.PizzaFactory.DataAccess.Model;
using Ucas.TechTest.PizzaFactory.Restaurant;

namespace Ucas.TechTest.PizzaFactory.Consumers.Kitchen
{
    public class OnlineOrdersWaiter : IPizzeriaWaiter
    {
        private IOrdersReader _ordersReader;
        private ConcurrentQueue<IPizzaOrder> _buffer;
        
        public OnlineOrdersWaiter(IOrdersReader ordersReader)
        {
            _ordersReader = ordersReader;
            _buffer = new ConcurrentQueue<IPizzaOrder>();
        }

        public IPizzaOrder GetNextOrder()
        {
            if (this._buffer.IsEmpty)
            {
                var pendingOrder = this._ordersReader.GetOrders(
                    OrderStatus.Pending).FirstOrDefault();

                if (pendingOrder != null && pendingOrder.Pizzas.Any())
                {
                    foreach (var pizzaOrder in pendingOrder.Pizzas)
                    {
                        this._buffer.Enqueue(pizzaOrder);
                    }
                }
            }

            return this._buffer.TryDequeue(out var nextOrder) ? nextOrder : null;
        }
    }
}