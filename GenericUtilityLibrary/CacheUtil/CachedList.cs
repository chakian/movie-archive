using System.Collections.Generic;

namespace com.cagdaskorkut.utility.CacheUtil {
	class CachedList : ICachedList {
		public List<string> ItemList { get; set; }

		public void CacheIt ( System.Collections.Generic.List<string> items ) {
			throw new System.NotImplementedException ( );
		}

		public System.Collections.Generic.List<string> GetCached ( ) {
			throw new System.NotImplementedException ( );
		}
	}
}