using Microsoft.ApplicationInsights;
using System.Web.Http.ExceptionHandling;

namespace Axinom.ClearKeyServer
{
    public class ReportingExceptionHandler : ExceptionHandler
    {
        private static TelemetryClient _client = new TelemetryClient();

        public override void Handle(ExceptionHandlerContext context)
        {
            _client.TrackException(context.Exception);

            base.Handle(context);
        }
    }
}