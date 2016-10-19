using System;

namespace Axinom.ClearKeyServer
{
	public static class ConvertHelpers
	{
		public static string ByteArrayToBase64Url(byte[] bytes)
		{
			if (bytes == null)
				throw new ArgumentNullException(nameof(bytes));

			// We also remove padding because most usages of base64url do not want it.
			return Convert.ToBase64String(bytes).Replace('/', '_').Replace('+', '-').TrimEnd('=');
		}

		public static byte[] Base64UrlToByteArray(string base64url)
		{
			if (base64url == null)
				throw new ArgumentNullException(nameof(base64url));

			// .NET implementation requires padding, so let's add it back if needed.
			var padding = new string('=', 4 - (base64url.Length % 4));

			return Convert.FromBase64String(base64url.Replace('_', '/').Replace('-', '+') + padding);
		}
	}
}