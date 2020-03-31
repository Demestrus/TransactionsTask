using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using TransactionTask.Core.Exceptions;

namespace TransactionTask.WebApi.Exceptions
{
    public class ServerErrorExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;

            switch (exception)
            {
                case BadRequestException badRequestException:
                    context.Result = new TextPlainErrorResult
                    {
                        Request = context.ExceptionContext.Request,
                        Content = badRequestException.Message,
                        StatusCode = HttpStatusCode.BadRequest
                    };
                    break;
                case NotFoundException notFoundException:
                    context.Result = new TextPlainErrorResult
                    {
                        Request = context.ExceptionContext.Request,
                        Content = notFoundException.Message,
                        StatusCode = HttpStatusCode.NotFound
                    };
                    break;
                default:
                    context.Result = new TextPlainErrorResult
                    {
                        Request = context.ExceptionContext.Request,
                        Content = "Something went wrong."
                    };
                    break;
            }
        }
        
        private class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public string Content { get; set; }
            public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(StatusCode)
                {
                    Content = new StringContent(Content), 
                    RequestMessage = Request
                };
                return Task.FromResult(response);
            }
        }
    }
}