using System.Web.Http;
using AutoMapper;
using TransactionTask.Core.BusinessLogic;
using TransactionTask.Core.Models;
using TransactionTask.WebApi.Mapper;
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

            var mapperConfig = new MapperConfiguration(
                cfg => cfg.AddProfile(new UserProfile())
            );

            container.RegisterType<IMapper, AutoMapper.Mapper>(new SingletonLifetimeManager());
            container.RegisterFactory<IMapper>(s => new AutoMapper.Mapper(mapperConfig));
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}