using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Axinom.ClearKeyServer
{
    public class UserFriendlyExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is BadRequestException)
            {
                // Report as-is.
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, context.Exception.Message);
            }
            else if (context.Exception is OperationCanceledException || context.Exception is TaskCanceledException)
            {
                // This just means something timed out. We don't always know what but just say it was a timeout.
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Timeout.");
            }
        }
    }
}