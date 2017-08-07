using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Axinom.ClearKeyServer
{
    public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			// Enable Application Insights if configured.
			if (!string.IsNullOrWhiteSpace(WebConfigurationManager.AppSettings["InstrumentationKey"]))
				TelemetryConfiguration.Active.InstrumentationKey = WebConfigurationManager.AppSettings["InstrumentationKey"];

			var configuration = GlobalConfiguration.Configuration;

            // Automatically log exceptions.
            configuration.Filters.Add(new ReportingExceptionFilterAttribute());

            // Automatically transform BadRequestException to the appropriate HTTP response.
            configuration.Filters.Add(new UserFriendlyExceptionsFilterAttribute());

            // Log all last-chance exceptions while we're at it.
            configuration.Services.Replace(typeof(IExceptionHandler), new ReportingExceptionHandler());

            // JSON only!
            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);

			configuration.Routes.MapHttpRoute(
				name: "Basic",
				routeTemplate: "{controller}"
			);
		}

        protected void Application_Error(object sender, EventArgs e)
        {
            new TelemetryClient().TrackException(Server.GetLastError());
        }
    }
}