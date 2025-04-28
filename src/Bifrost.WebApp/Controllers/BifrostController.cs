using Bifrost.Commands;
using Bifrost.Read;
using Bifrost.WebApp.Bifrost;
using Bifrost.WebApp.Read;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Ninject;

namespace Bifrost.WebApp.Controllers
{
    [ApiController]
    [Route("bifrost")]
    public class BifrostController : ControllerBase
    {
        private readonly ICommandCoordinator commandCoordinator;
        private readonly IQueryCoordinator queryCoordinator;

        public BifrostController(ICommandCoordinator commandCoordinator, IQueryCoordinator queryCoordinator)
        {
            this.commandCoordinator = commandCoordinator;
            this.queryCoordinator = queryCoordinator;
        }

        public StandardKernel Kernel { get; }

        // GET /bifrost
        [HttpGet]
        public ActionResult<QueryResult> Query()
        {
            if (queryCoordinator is not null)
            {
                var res = queryCoordinator.Execute(new MyQuery(), new PagingInfo());
                return res;

            }
            return null!;
        }

        [HttpPost]
        public ActionResult<CommandResult> CreateCommand([FromBody] string something)
        {
            if (commandCoordinator is not null)
            {
                var res = commandCoordinator.Handle(new MyCommand() { Something = something });
                return res;

            }
            return null!;
        }

    }
}

