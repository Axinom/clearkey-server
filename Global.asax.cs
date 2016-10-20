using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Web.Configuration;
using System.Web.Http;

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

			// JSON only!
			configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);

			configuration.Routes.MapHttpRoute(
				name: "Basic",
				routeTemplate: "{controller}"
			);
		}
	}
}