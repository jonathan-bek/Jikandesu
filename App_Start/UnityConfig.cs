using System;
using System.Collections.Generic;
using System.Linq;
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
            var controllerTypes = GetControllerTypes(repositoryAssembly);
            var nonControllerTypes = repositoryAssembly.GetTypes().Except(controllerTypes);

            //Register controllers to resolve per request
            container.RegisterTypes(controllerTypes,
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.PerResolve);

            //Register other types to resolve only once (singleton, default setting)
            container.RegisterTypes(nonControllerTypes,
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);

            //Register others here in the future
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IEnumerable<Type> GetControllerTypes(Assembly assembly)
        {
            var controllerTypes = assembly.GetTypes()
                .Where(x => typeof(Controller).IsAssignableFrom(x));
            return controllerTypes;
        }
    }
}
