using System.Web.Http;
using TransactionTask.Core.BusinessLogic;
using TransactionTask.Core.Models;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace TransactionTask.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<IUsersService, UsersService>(new TransientLifetimeManager());
            container.RegisterType<TaskDbContext>(new TransientLifetimeManager());
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}