using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Ucas.TechTest.PizzaFactory.Model;
using Ucas.TechTest.PizzaFactory.Restaurant;

namespace Ucas.TechTest.PizzaFactory.WebApi.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IOrderWriter _orderWriter;
        private readonly IOrderReader _orderReader;
        private readonly IOrdersReader _ordersReader;
        private readonly IOrderUpdater _ordersUpdater;

        public OrdersController(ILogger logger, IOrderWriter orderWriter, IOrderReader orderReader, IOrdersReader ordersReader, IOrderUpdater ordersUpdater)
        {
            _logger = logger;
            _orderWriter = orderWriter;
            _orderReader = orderReader;
            _ordersReader = ordersReader;
            _ordersUpdater = ordersUpdater;
        }

        [Route("create")]
        [HttpPost]
        public Task<IOrder> Post([FromBody] IEnumerable<IPizzaOrder> orderItems, CancellationToken cancellationToken)
        {
            return this._orderWriter.CreateOrderAsync(orderItems, cancellationToken);
        }

        [HttpGet]
        [Route("{orderNumber}")]
        public Task<IOrder> GetOrder([FromRoute] string orderNumber, CancellationToken cancellationToken)
        {
            return this._orderReader.GetOrderAsync(orderNumber, cancellationToken);
        }

        [HttpGet]
        [Route("orders/{orderStatus}")]
        public Task<IEnumerable<IOrder>> GetOrders(OrderStatus orderStatus, CancellationToken cancellationToken)
        {
            return this._ordersReader.GetOrdersAsync(orderStatus, cancellationToken);
        }

        [HttpPut]
        [Route("order/{orderNumber}/{orderStatus}")]
        public Task UpdateOrder(OrderStatus orderStatus, CancellationToken cancellationToken)
        {
            return this._ordersUpdater.UpdateOrderAsync(orderStatus, cancellationToken);
        }
    }
}