using Newtonsoft.Json;

namespace Axinom.ClearKeyServer.Model
{
	public sealed class ContentKey
	{
		[JsonProperty("kty")]
		public string Type { get; } = "oct";

		[JsonProperty("k")]
		public string ValueAsBase64Url { get; set; }

		[JsonProperty("kid")]
		public string IdAsBase64Url { get; set; }
	}
}