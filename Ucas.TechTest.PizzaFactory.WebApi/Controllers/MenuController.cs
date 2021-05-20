using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ucas.TechTest.PizzaFactory.Core.Model;
using Ucas.TechTest.PizzaFactory.DataAccess.Model;

namespace Ucas.TechTest.PizzaFactory.WebApi.Controllers
{
    [ApiController]
    [Route("menu")]
    public class MenuController : ControllerBase
    {
        private readonly ILogger<MenuController> _logger;
        private readonly IMenuReader _menuReader;

        public MenuController(ILogger<MenuController> logger, IMenuReader menuReader)
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