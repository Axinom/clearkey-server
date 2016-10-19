using System;
using System.Linq;

namespace Axinom.ClearKeyServer
{
	public static class GuidHelpers
	{
		/// <summary>
		/// Deserializes a GUID from a byte array that uses the big endian format for all components.
		/// This format is often used by non-Microsoft tooling.
		/// </summary>
		public static Guid FromBigEndianByteArray(byte[] bytes)
		{
			if (bytes == null)
				throw new ArgumentNullException(nameof(bytes));

			if (!BitConverter.IsLittleEndian)
				throw new InvalidOperationException("This method has not been tested on big endian machines and likely would not operate correctly.");

			var bytesCopy = bytes.ToArray(); // Copy, to ensure that input is not modified.
			FlipSerializedGuidEndianness(bytesCopy);

			return new Guid(bytesCopy);
		}

		public static void FlipSerializedGuidEndianness(byte[] bytes)
		{
			// Some encryption tools (e.g. MP4Box) use GUIDs with different byte orders from .NET.
			// Two variants exist: "big endian" and "little endian", with the latter as the defualt for .NET.
			// A GUID consists of 5 groups of bytes, of which only the first 3 are byte order dependent.
			Array.Reverse(bytes, 0, 4);
			Array.Reverse(bytes, 4, 2);
			Array.Reverse(bytes, 6, 2);
		}

	}
}