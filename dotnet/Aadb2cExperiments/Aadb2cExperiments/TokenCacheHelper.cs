using System.IO;
using Microsoft.Identity.Client;

namespace Aadb2cExperiments
{
    public static class TokenCacheHelper
    {
        private static readonly object FileLock = new object();
        private static TokenCache _usertokenCache;

        public static string CacheFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location + "msalcache.txt";

        public static TokenCache GetUserCache()
        {
            if (_usertokenCache == null)
            {
                _usertokenCache = new TokenCache();
                _usertokenCache.SetBeforeAccess(BeforeAccessNotification);
                _usertokenCache.SetAfterAccess(AfterAccessNotification);
            }
            return _usertokenCache;
        }

        public static void DeleteUserCache()
        {
            lock (FileLock)
            {
                if (File.Exists(CacheFilePath))
                {
                    File.Delete(CacheFilePath);
                }
            }
        }

        public static void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            lock (FileLock)
            {
                args.TokenCache.Deserialize(File.Exists(CacheFilePath)
                    ? File.ReadAllBytes(CacheFilePath)
                    : null);
            }
        }

        public static void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
            if (args.TokenCache.HasStateChanged)
            {
                lock (FileLock)
                {
                    // reflect changes in the persistent store
                    File.WriteAllBytes(CacheFilePath, args.TokenCache.Serialize());

                    // once the write operation takes place restore the HasStateChanged bit to false
                    args.TokenCache.HasStateChanged = false;
                }
            }
        }
    }
}
