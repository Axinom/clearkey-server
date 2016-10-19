using System;
using System.Web.Http;

namespace Axinom.ClearKeyServer
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
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