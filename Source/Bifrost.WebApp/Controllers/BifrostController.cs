using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.WebApp.Bifrost;
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
        public ActionResult<CommandResult> GetBifrost()
        {
            var s = GlobalVariables.DiContainer;
            var commandCoordinator = s.Get<ICommandCoordinator>();
            if (commandCoordinator is not null)
            {
                var res = commandCoordinator.Handle(new MyCommand() { Something = "" });
                return res;

            }
            return null!;
        }
    }
}

