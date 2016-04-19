//using System;
//using System.Reflection;
//using System.Runtime.Caching;
//using System.Text;

//namespace com.cagdaskorkut.utility.Caching {
//    [Serializable]
//    public class CacheAttribute : Attribute {
//        public const int OneMonth = 2630000;
//        public const int OneWeek = 604800;
//        public const int OneDay = 86400;
//        public const int OneHour = 3600;

//        [NonSerialized]
//        private object syncRoot;
//        private static string DefaultCacheName = "DEFAULT_CACHE";
//        private string _CacheName;
//        private string _CachePrefix;
//        private string _IgnoredParameters;

//        public double TimeoutSeconds = CacheAttribute.OneMonth;

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
//        public string CachePrefix {
//            get {
//                if ( _CachePrefix == null )
//                    _CachePrefix = "CAGDAS_";
//                return _CachePrefix;
//            }
//        }
//        public string IgnoredParameters {
//            get {
//                return _IgnoredParameters;
//            }
//            set {
//                _IgnoredParameters = value;
//            }
//        }

//        //public override void RuntimeInitialize ( MethodBase method ) {
//        //    syncRoot = new object ( );
//        //}

//        //public override void OnInvoke ( EventArgs args ) {
//        //    var key = GenerateKey ( args );
//        //    MemoryCache cache = CacheManager.GetCache ( this.CacheName );
//        //    object cachedValue = cache.Get ( key );

//        //    if ( cachedValue != null ) {
//        //        //args.ReturnValue = cachedValue;
//        //    } else {
//        //        lock ( syncRoot ) {
//        //            if ( cachedValue == null ) {
//        //                //var returnVal = args.Invoke ( args.Arguments );
//        //                //args.ReturnValue = returnVal;

//        //                //if ( returnVal != null ) {
//        //                //    CacheItemPolicy policy = new CacheItemPolicy ( );
//        //                //    policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromSeconds ( TimeoutSeconds );

//        //                //    cache.Add ( key, returnVal, policy );
//        //                //}
//        //            } else {
//        //                //args.ReturnValue = cachedValue;
//        //            }
//        //        }
//        //    }
//        //}

//        private string GenerateKey(EventArgs args) {
//            StringBuilder sb = new StringBuilder ( );
//            //sb.Append ( args.Method.Name );
//            //foreach ( var argument in args.Arguments ) {
//            //    sb.Append ( "_" );
//            //    if ( argument == null ) {
//            //        sb.Append ( "null" );
//            //    } else if ( argument is string ) {
//            //        sb.Append ( argument.ToString ( ) );
//            //    } else {
//            //        sb.Append ( argument.GetHashCode ( ) );
//            //    }
//            //}
//            return sb.ToString ( );
//        }
//    }
//}