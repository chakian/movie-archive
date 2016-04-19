using System.Collections.Generic;

namespace com.cagdaskorkut.utility.CacheUtil {
	public interface ICachedList {
		List<string> ItemList { get; set; }

		void CacheIt ( List<string> items );
		List<string> GetCached ( );
	}
}