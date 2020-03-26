using System.Web;
using System.Web.Mvc;
using TransactionTask.Core.Models;
using TransactionTask.WebApi.Exceptions;

namespace TransactionTask.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
