using System;

namespace MArchiveLibrary {
	public partial class tblMovie {
		public override string ToString ( ) {
			if ( string.IsNullOrEmpty ( movieOriginalName ) == false )
				return movieOriginalName;
			else if ( string.IsNullOrEmpty ( movieEnglishName ) == false )
				return movieEnglishName;
			else if ( string.IsNullOrEmpty ( movieTurkishName ) == false )
				return movieTurkishName;
			else
				return string.Empty;
		}
	}
}