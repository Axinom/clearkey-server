using Axinom.Cpix;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Axinom.ClearKeyServer
{
	/// <summary>
	/// We load all *.xml files from App_Data and serve any keys we can read from them.
	/// </summary>
	public sealed class KeyDatabase
	{
		/// <summary>
		/// The singleton instance. NB! Only loaded once!
		/// You must recycle the application to reload the key database.
		/// </summary>
		public static readonly KeyDatabase Current = new KeyDatabase();

		public IReadOnlyDictionary<Guid, byte[]> Keys { get; }

		private KeyDatabase()
		{
			var keys = new Dictionary<Guid, byte[]>();
			Keys = keys;

			var appDataPath = HttpContext.Current.Server.MapPath("~/App_Data");

			if (!Directory.Exists(appDataPath))
				return; // Nothing to load, no data exists!

			foreach (var cpixFilePath in Directory.GetFiles(appDataPath, "*.xml"))
			{
				var document = CpixDocument.Load(cpixFilePath);

				if (!document.ContentKeysAreReadable)
					continue;

				foreach (var key in document.ContentKeys)
					keys[key.Id] = key.Value;
			}
		}
	}
}