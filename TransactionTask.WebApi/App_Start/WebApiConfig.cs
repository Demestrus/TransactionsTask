using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using TransactionTask.WebApi.Exceptions;

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
        }
    }
}
