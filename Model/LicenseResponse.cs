using Newtonsoft.Json;

namespace Axinom.ClearKeyServer.Model
{
	public sealed class LicenseResponse
	{
		[JsonProperty("keys")]
		public ContentKey[] Keys { get; set; }

		[JsonProperty("type")]
		public string SessionType { get; set; }
	}
}