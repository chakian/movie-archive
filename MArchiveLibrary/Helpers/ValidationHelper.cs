using System.Text.RegularExpressions;
using System.Text;

namespace MArchiveLibrary.Helpers
{
	public static class ValidationHelper {
		public static string replaceNonEnglishCharacters (string input ) {
            string returnValue = input.ToLower ( );

            int i = returnValue.IndexOfAny ( new char[] { 'ş', 'ç', 'ö', 'ğ', 'ü', 'ı', 'â', 'á', 'à', 'ä', 'ã', 'ê', 'é', 'è' } );
			//if any non-english charr exists,replace it with proper char
			if ( i > -1 ) {
				StringBuilder outPut = new StringBuilder ( returnValue );
				outPut.Replace ( 'ö', 'o' );
				outPut.Replace ( 'ç', 'c' );
				outPut.Replace ( 'ş', 's' );
				outPut.Replace ( 'ı', 'i' );
				outPut.Replace ( 'ğ', 'g' );
				outPut.Replace ( 'ü', 'u' );
				outPut.Replace ( 'â', 'a' );
				outPut.Replace ( 'à', 'a' );
				outPut.Replace ( 'á', 'a' );
				outPut.Replace ( 'ä', 'a' );
				outPut.Replace ( 'ã', 'a' );
				outPut.Replace ( 'ê', 'e' );
				outPut.Replace ( 'é', 'e' );
				outPut.Replace ( 'è', 'e' );
				returnValue = outPut.ToString ( );
			}
			// if there are other invalid chars, convert them into blank spaces
			returnValue = Regex.Replace ( returnValue, @"[^a-z0-9\s-]", "" );
			// convert multiple spaces and hyphens into one space       
			returnValue = Regex.Replace ( returnValue, @"[\s-]+", " " ).Trim ( );
			// add hyphens
			returnValue = Regex.Replace ( returnValue, @"\s", "-" );

			return returnValue;
		}
	}
}