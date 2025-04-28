using Bifrost.Configuration;
using Bifrost.Execution;
using Ninject;
using Bifrost.Ninject;

namespace Bifrost.WebApp.Bifrost
{
    public class ContainerCreator : ICanCreateContainer
    {
        public IContainer CreateContainer()
        {
            //var structureMap = new StructureMap.Container();
            //var container = new Container(structureMap);

            var kernel = new StandardKernel();
            var container = new Container(kernel);
            GlobalVariables.DiContainer = container;



            return container;
        }
    }
}
