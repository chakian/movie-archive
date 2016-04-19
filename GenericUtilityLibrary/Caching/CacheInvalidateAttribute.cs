//using System;
//using System.Reflection;

//namespace com.cagdaskorkut.utility.Caching {
//    [Serializable]
//    [AttributeUsage ( AttributeTargets.Method, AllowMultiple = false )]
//    public class CacheInvalidateAttribute : Attribute {
//        [NonSerialized]
//        private object syncRoot;

//        private static string DefaultCacheName = "DEFAULT_CACHE";
//        private string _CacheName;

//        public string CacheName {
//            get {
//                if ( _CacheName == null )
//                    _CacheName = DefaultCacheName;
//                return this.CachePrefix + _CacheName;
//            }
//            set {
//                _CacheName = value;
//            }
//        }

//        private string _CachePrefix;

//        public string CachePrefix {
//            get {
//                if ( _CachePrefix == null )
//                    _CachePrefix = "CAGDAS_";
//                return _CachePrefix;
//            }
//        }

//        //public override void RuntimeInitialize ( MethodBase method ) {
//        //    syncRoot = new object ( );
//        //}

//        //public override void OnSuccess ( EventArgs args ) {
//        //    CacheManager.InvalidateCache ( CacheName );
//        //}
//    }
//}