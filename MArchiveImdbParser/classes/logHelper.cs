using System;
using System.IO;

namespace MArchiveImdbParser {
	public static class logHelper {
		public static string createLogFileName ( string input ) {
			return System.Text.RegularExpressions.Regex.Replace ( input, @"[/|\\|\||:|*|?|""|<|>]", "_" ) + ".txt";
		}
		public static void logLine ( TextWriter tw, string message, bool includeDate ) {
			tw.WriteLine ( String.Format ( "{0}{1}",
				( includeDate ? DateTime.Now.ToString ( ) + "\t" : "" ),
				message ) );
		}
		public static void logLine ( TextWriter tw, string message ) {
			logLine ( tw, message, true );
		}
		public static void logLine ( TextWriter tw ) {
			tw.WriteLine ( );
		}
		public static void logException ( TextWriter tw, Exception e ) {
			logLine ( tw );
			logLine ( tw, "Exception occured" );
			logLine ( tw, e.Message, false );
			logLine ( tw, e.StackTrace, false );
			logLine ( tw );
		}
	}
}
