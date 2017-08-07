using Microsoft.ApplicationInsights;
using System.Web.Http.Filters;

namespace Axinom.ClearKeyServer
{
    public class ReportingExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static TelemetryClient _client = new TelemetryClient();

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            _client.TrackException(actionExecutedContext.Exception);
        }
    }
}