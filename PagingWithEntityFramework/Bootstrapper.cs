using System.Web.Mvc;
using Microsoft.Practices.Unity;
using PagingWithEntityFramework.Business;
using PagingWithEntityFramework.DAL;
using Unity.Mvc4;

namespace PagingWithEntityFramework
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    
            RegisterTypes(container);

            return container;
        }

        private static void RegisterTypes(IUnityContainer container)
        {            
            // business layer
            container.RegisterType<ErrorService>();

            // data access layer
            container.RegisterType<ErrorContext>();
        }
    }
}