using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace com.cagdaskorkut.utility {
	public class RegexHelper {
		public static string MatchSingle ( string input, Regex regex ) {
			try {
				return regex.Matches ( input )[0].Groups[1].Value;
			} catch {
				return "";
			}
		}

		public static List<string> MatchMultiple ( string input, Regex regex ) {
			try {
				List<string> resultList = new List<string> ( );
				MatchCollection mc = regex.Matches ( input );
				foreach ( Match item in mc ) {
					resultList.Add ( item.Groups[1].Value );
				}
				return resultList;
			} catch {
				return new List<string> ( );
			}
		}
	}
}