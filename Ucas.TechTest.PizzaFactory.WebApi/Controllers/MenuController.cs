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
    [Route("menu")]
    public class MenuController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMenuReader _menuReader;

        public MenuController(ILogger logger, IMenuReader menuReader, IOrderWriter orderWriter, IOrderReader orderReader, IOrdersReader ordersReader, IOrderUpdater ordersUpdater)
        {
            _logger = logger;
            _menuReader = menuReader;
        }

        [HttpGet]
        public Task<IPizzaMenu> GetMenu(CancellationToken cancellationToken)
        {
            return this._menuReader.GetMenuAsync(cancellationToken);
        }
    }
}