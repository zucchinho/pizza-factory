using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMenuReader _menuReader;
        private readonly IOrderWriter _orderWriter;
        private readonly IOrderReader _orderReader;

        public OrdersController(ILogger logger, IMenuReader menuReader, IOrderWriter orderWriter, IOrderReader orderReader)
        {
            _logger = logger;
            _menuReader = menuReader;
            _orderWriter = orderWriter;
            _orderReader = orderReader;
        }

        [Route("menu")]
        [HttpGet]
        public Task<IPizzaMenu> Get(CancellationToken cancellationToken)
        {
            return this._menuReader.GetMenuAsync(cancellationToken);
        }

        [Route("create")]
        [HttpPost]
        public Task<IOrder> Post([FromBody] IEnumerable<IPizzaOrder> orderItems, CancellationToken cancellationToken)
        {
            return this._orderWriter.CreateOrderAsync(orderItems, cancellationToken);
        }

        [HttpGet]
        public Task<IOrder> Post(string orderNumber, CancellationToken cancellationToken)
        {
            return this._orderReader.GetOrderAsync(orderNumber, cancellationToken);
        }
    }
}