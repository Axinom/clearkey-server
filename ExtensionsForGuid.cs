using System;

namespace Axinom.ClearKeyServer
{
	public static class ExtensionsForGuid
	{
		/// <summary>
		/// Serializes the GUID to a byte array, using the big endian format for all components.
		/// This format is often used by non-Microsoft tooling.
		/// </summary>
		public static byte[] ToBigEndianByteArray(this Guid guid)
		{
			if (guid == null)
				throw new ArgumentNullException(nameof(guid));

			if (!BitConverter.IsLittleEndian)
				throw new InvalidOperationException("This method has not been tested on big endian machines and likely would not operate correctly.");

			var bytes = guid.ToByteArray();

			GuidHelpers.FlipSerializedGuidEndianness(bytes);

			return bytes;
		}
	}
}