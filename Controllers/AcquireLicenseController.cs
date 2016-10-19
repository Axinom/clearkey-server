using Axinom.ClearKeyServer.Model;
using System;
using System.Linq;
using System.Web.Http;

namespace Axinom.ClearKeyServer.Controllers
{
	public sealed class AcquireLicenseController : ApiController
	{
		[HttpPost]
		public LicenseResponse Post([FromBody] LicenseRequest request)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			request.Validate();

			return new LicenseResponse
			{
				// Whatever session type was requested shall be in the response.
				SessionType = request.SessionType,

				Keys = request.KeyIds.Select(kid =>
				{
					if (!KeyDatabase.Current.Keys.ContainsKey(kid))
						throw new ArgumentException("The key database does not contain the key: " + kid);

					return new ContentKey
					{
						IdAsBase64Url = ConvertHelpers.ByteArrayToBase64Url(kid.ToBigEndianByteArray()),
						ValueAsBase64Url = ConvertHelpers.ByteArrayToBase64Url(KeyDatabase.Current.Keys[kid])
					};
				}).ToArray()
			};
		}
	}
}