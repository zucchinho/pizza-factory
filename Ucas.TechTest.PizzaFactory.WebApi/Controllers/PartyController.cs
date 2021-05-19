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
    public class PartyController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IPartyWriter _partyWriter;

        public PartyController(ILogger logger, IPartyWriter partyWriter)
        {
            _logger = logger;
            _partyWriter = partyWriter;
        }

        [HttpPost]
        public Task<IParty> Create(int tableNumber, int partySize, CancellationToken cancellationToken)
        {
            return this._partyWriter.CreatePartyAsync(tableNumber, partySize, cancellationToken);
        }
    }
}