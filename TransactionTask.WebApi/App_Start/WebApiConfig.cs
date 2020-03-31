using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using TransactionTask.Core.Models;
using TransactionTask.WebApi.Exceptions;
using TransactionTask.WebApi.Models;

namespace TransactionTask.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            config.Services.Add(typeof(IExceptionLogger), new DbExceptionLogger());
            
            config.Services.Replace(typeof(IExceptionHandler), new ServerErrorExceptionHandler());
            
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            
            builder.EntitySet<User>("Users")
                .EntityType
                .Count()
                .Filter()
                .OrderBy()
                .Expand()
                .Select();

            builder.ComplexType<UserDto>();
            builder.ComplexType<ExistingUserDto>();

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "odata",
                model: builder.GetEdmModel());
            
            config.AddODataQueryFilter();
        }
    }
}
