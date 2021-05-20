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
        private readonly IOrdersReader _ordersReader;
        private readonly IOrderUpdater _orderUpdater;
        private readonly IOrderReader _orderReader;
        private readonly ConcurrentQueue<KeyValuePair<string, IPizzaOrder>> _buffer;
        private readonly ConcurrentDictionary<string, List<IPizzaOrder>> _receivedOrders;
        
        public OnlineOrdersWaiter(IOrdersReader ordersReader, IOrderUpdater orderUpdater, IOrderReader orderReader)
        {
            _ordersReader = ordersReader;
            _orderUpdater = orderUpdater;
            _orderReader = orderReader;

            _buffer = new ConcurrentQueue<KeyValuePair<string, IPizzaOrder>>();
            _receivedOrders = new ConcurrentDictionary<string, List<IPizzaOrder>>();
        }

        public IPizzaOrder GetNextOrder()
        {
            // check the buffer
            if (this._buffer.IsEmpty)
            {
                // buffer empty so get next pending order
                var pendingOrder = this._ordersReader
                    .GetOrders(OrderStatus.Pending)
                    .FirstOrDefault(o => !this._receivedOrders.ContainsKey(o.OrderNumber));

                if (pendingOrder != null && pendingOrder.Pizzas.Any())
                {
                    // add the order items to the queue
                    foreach (var pizzaOrder in pendingOrder.Pizzas)
                    {
                        this._buffer.Enqueue(new KeyValuePair<string, IPizzaOrder>(
                            pendingOrder.OrderNumber,
                            pizzaOrder));
                    }
                }
            }

            if (this._buffer.TryDequeue(out var nextOrder))
            {
                // add or update the received track for this order number
                var received = this._receivedOrders.AddOrUpdate(
                    nextOrder.Key,
                    new List<IPizzaOrder>{ nextOrder.Value },
                    (key, current) => current.Append(nextOrder.Value).ToList());

                if (received.Count > 0)
                {
                    var correspondingOrder = this._orderReader.GetOrder(nextOrder.Key);
                    
                    // check if all the order items have been received
                    if (received.Count >= correspondingOrder.Pizzas.Count())
                    {
                        // update the status of the order itself to received
                        this._orderUpdater.UpdateOrder(
                            correspondingOrder.OrderNumber,
                            OrderStatus.Received);
                    }
                }
                
                return nextOrder.Value;
            }

            return null;
        }
    }
}