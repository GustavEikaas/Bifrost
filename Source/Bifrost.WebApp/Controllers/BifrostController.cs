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

        public BifrostController(StandardKernel kernel)
        {
            Kernel = kernel;
        }

        public StandardKernel Kernel { get; }

        // GET /bifrost
        [HttpGet]
        public ActionResult<QueryResult> Query()
        {
            //HACK: remove Ninject and rewrite to use standard IOC
            var s = GlobalVariables.DiContainer;
            var queryCoordinator = s.Get<IQueryCoordinator>();
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
            //HACK: remove Ninject and rewrite to use standard IOC
            var s = GlobalVariables.DiContainer;
            var commandCoordinator = s.Get<ICommandCoordinator>();
            if (commandCoordinator is not null)
            {
                var res = commandCoordinator.Handle(new MyCommand() { Something = something });
                return res;

            }
            return null!;
        }

    }
}

