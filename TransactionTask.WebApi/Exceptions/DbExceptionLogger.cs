using System;
using System.Web.Http.ExceptionHandling;
using TransactionTask.Core.Models;

namespace TransactionTask.WebApi.Exceptions
{
    public class DbExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            using (var db = new TaskDbContext())
            {
                var exception = context.ExceptionContext.Exception;
                var logEntry = new LogEntry
                {
                    TimeStamp = DateTime.UtcNow.Ticks,
                    ErrorMsg = exception.Message,
                    StackTrace = exception.StackTrace
                };

                db.Log.Add(logEntry);
                db.SaveChanges();
            }
        }
    }
}