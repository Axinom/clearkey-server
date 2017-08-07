using Axinom.ClearKeyServer.Model;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Axinom.ClearKeyServer.Controllers
{
    public sealed class AcquireLicenseController : ApiController
    {
        private static readonly TelemetryClient _telemetry = new TelemetryClient();

        [HttpPost]
        public LicenseResponse Post([FromBody] LicenseRequest request)
        {
            if (request == null)
                throw new BadRequestException("Request body could not be deserialized as a license request.");

            request.Validate();

            _telemetry.TrackEvent("LicenseRequest", new Dictionary<string, string>
            {
                { "KeySessionType", request.SessionType }
            }, new Dictionary<string, double>
            {
                { "RequestedKeyCount", request.KeyIdsAsBase64Url.Length }
            });

            // 404 if a key is missing
            foreach (var requestedKey in request.KeyIds)
                if (!KeyDatabase.Current.Keys.ContainsKey(requestedKey))
                    throw new HttpResponseException(new HttpResponseMessage
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        ReasonPhrase = "Key is not in database: " + requestedKey
                    });

            return new LicenseResponse
            {
                // Whatever session type was requested shall be in the response.
                SessionType = request.SessionType,

                Keys = request.KeyIds.Select(kid => new ContentKey
                {
                    IdAsBase64Url = ConvertHelpers.ByteArrayToBase64Url(kid.ToBigEndianByteArray()),
                    ValueAsBase64Url = ConvertHelpers.ByteArrayToBase64Url(KeyDatabase.Current.Keys[kid])
                }).ToArray()
            };
        }
    }
}