using System.IO;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Security.Cryptography;

namespace AdalExperiments
{
	public class LocalFileTokenCache : TokenCache
	{
		public string CacheFilePath;
		private static readonly object FileLock = new object();

		// Initializes the cache against a local file.
		// If the file is already rpesent, it loads its content in the ADAL cache
		public LocalFileTokenCache(string filePath = @".\TokenCache.dat")
		{
			CacheFilePath = filePath;
			AfterAccess += AfterAccessNotification;
			BeforeAccess += BeforeAccessNotification;

			lock (FileLock)
			{
				Deserialize(File.Exists(CacheFilePath) ? ProtectedData.Unprotect(File.ReadAllBytes(CacheFilePath), null, DataProtectionScope.CurrentUser) : null);
			}
		}

		// Empties the persistent store.
		public override void Clear()
		{
			base.Clear();
			File.Delete(CacheFilePath);
		}

		// Triggered right before ADAL needs to access the cache.
		// Reload the cache from the persistent store in case it changed since the last access.
		private void BeforeAccessNotification(TokenCacheNotificationArgs args)
		{
			lock (FileLock)
			{
				Deserialize(File.Exists(CacheFilePath) ? ProtectedData.Unprotect(File.ReadAllBytes(CacheFilePath), null, DataProtectionScope.CurrentUser) : null);
			}
		}

		// Triggered right after ADAL accessed the cache.
		private void AfterAccessNotification(TokenCacheNotificationArgs args)
		{
			// if the access operation resulted in a cache update
			if (HasStateChanged)
			{
				lock (FileLock)
				{
					// reflect changes in the persistent store
					File.WriteAllBytes(CacheFilePath, ProtectedData.Protect(this.Serialize(), null, DataProtectionScope.CurrentUser));
					// once the write operation took place, restore the HasStateChanged bit to false

					HasStateChanged = false;
				}
			}
		}
	}
}
