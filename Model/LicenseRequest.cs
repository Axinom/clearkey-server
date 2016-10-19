using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Axinom.ClearKeyServer.Model
{
	public sealed class LicenseRequest
	{
		[JsonProperty("kids")]
		public string[] KeyIdsAsBase64Url { get; set; }

		[JsonProperty("type")]
		public string SessionType { get; set; }

		public IEnumerable<Guid> KeyIds => KeyIdsAsBase64Url?.Select(kid => GuidHelpers.FromBigEndianByteArray(ConvertHelpers.Base64UrlToByteArray(kid)));

		private static readonly string[] ValidSessionTypes = new[] { "temporary", "persistent-usage-record", "persistent-license" };

		public void Validate()
		{
			if (!ValidSessionTypes.Contains(SessionType))
				throw new NotSupportedException("Unsupported key session type: " + SessionType);

			if (KeyIdsAsBase64Url.Length == 0)
				throw new NotSupportedException("No keys were requested in the license request.");

			try
			{
				// This has a side-effect of ensuring they all parse into a GUID.
				foreach (var kid in KeyIds)
				{
					if (kid == Guid.Empty)
						throw new NotSupportedException("A key ID consisting of zero bytes is not a supported key ID.");
				}
			}
			catch (NotSupportedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new NotSupportedException("Error in license request key IDs: " + ex.Message);
			}
		}
	}
}