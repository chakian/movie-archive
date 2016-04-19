using MArchiveLibrary.Caching;

namespace MArchive.BL {
    public abstract class BLBase {
        //protected static void WriteCache(string key, string functionName, object value) {
        //    CacheManager.WriteCache(key, functionName, value);
        //}
        protected static void WriteCache<T>(string key, string functionName, T value) {
            CacheManager.WriteCache<T>(key, functionName, value);
        }
        protected static object ReadCache(string key, string functionName) {
            return CacheManager.GetCache(key, functionName);
        }
        protected static T ReadCache<T>(string key, string functionName) {
            T cacheObject = default(T);

            var cache = ReadCache(key, functionName);
            try {
                cacheObject = (T)cache;
            } catch {
                CacheManager.InvalidateCache(key, functionName);
            }
            return cacheObject;
        }
        protected static void InvalidateCache(string key) {
            CacheManager.InvalidateAllCacheByKey(key);
        }
    }
}