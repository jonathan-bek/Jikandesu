using System.Reflection;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Unity.RegistrationByConvention;

namespace Jikandesu
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            var repositoryAssembly = Assembly.GetExecutingAssembly();
            container.RegisterTypes(repositoryAssembly.GetTypes(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            //  e.g. container.RegisterType<ITestService, TestService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
