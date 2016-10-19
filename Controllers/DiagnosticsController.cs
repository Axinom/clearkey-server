using System;
using System.Linq;
using System.Web.Http;

namespace Axinom.ClearKeyServer.Controllers
{
	public sealed class DiagnosticsController : ApiController
	{
		/// <summary>
		/// Just for diagnostics, returns all the key IDs that are loaded.
		/// </summary>
		public Guid[] Get()
		{
			return KeyDatabase.Current.Keys.Keys.OrderBy(k => k).ToArray();
		}
	}
}