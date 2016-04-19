//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Caching;

//namespace com.cagdaskorkut.utility.Caching
//{
//    public class CacheManager
//    {
//        public static MemoryCache GetCache(string cacheName)
//        {

//            object desiredCache = MemoryCache.Default.Get(cacheName);

//            if ((desiredCache == null) || ((desiredCache is MemoryCache) == false))
//            {
//                MemoryCache.Default.Remove(cacheName);
//                desiredCache = new MemoryCache(cacheName);
//                CacheItemPolicy policy = new CacheItemPolicy();

//                MemoryCache.Default.Add(cacheName, desiredCache, policy);
//            }

//            return (MemoryCache)desiredCache;
//        }

//        public static void InvalidateCache(string cacheName)
//        {
//            MemoryCache.Default.Remove(cacheName);
//        }

//        public static void InvalidateAll()
//        {
//            IList<MemoryCache> cacheList = CacheManager.GetCacheList();
//            foreach (var item in cacheList)
//            {
//                CacheManager.InvalidateCache(item.Name);
//            }
//        }

//        public static IList<MemoryCache> GetCacheList()
//        {
//            var allCache = MemoryCache.Default.ToList();
//            List<KeyValuePair<string, object>> cacheList = MemoryCache.Default.Where(g => g.Value is MemoryCache).ToList();
//            List<MemoryCache> retList = new List<MemoryCache>();

//            foreach (var item in cacheList)
//            {
//                if (item.Value is MemoryCache)
//                    retList.Add((MemoryCache)item.Value);
//            }

//            return retList;
//        }

//        public static CacheDTO GetCacheDTO()
//        {
//            CacheDTO cacheData = new CacheDTO();

//            IList<MemoryCache> cacheList = CacheManager.GetCacheList();
//            foreach (MemoryCache item in cacheList)
//            {
//                IEnumerable<KeyValuePair<string, object>> pairs = item.Where(g => g.Value != null || g.Value == null);
//                cacheData.CacheList.Add(new KeyValuePair<string, IEnumerable<KeyValuePair<string, object>>>(item.Name, pairs));
//            }
//            return cacheData;
//        }
//    }

//    public class CacheDTO
//    {
//        public List<KeyValuePair<string, IEnumerable<KeyValuePair<string, object>>>> CacheList { get; set; }

//        public CacheDTO()
//        {
//            this.CacheList = new List<KeyValuePair<string, IEnumerable<KeyValuePair<string, object>>>>();
//        }
//    }
//}