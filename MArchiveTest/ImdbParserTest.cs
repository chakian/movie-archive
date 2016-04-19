using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using MArchiveLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MArchiveTest {
	[TestClass]
	public class ImdbParserTest {
		private string getImdbMoviePageHtml ( ) {
			string tempHtml;
			//StreamReader sr = new StreamReader ( @"C:\Users\Chakian\Desktop\imdb\tt1315962.html" );
			//tempHtml = sr.ReadToEnd ( );

			string URL = "http://www.imdb.com/title/" + "tt1315962" + "/";
			StreamReader sr = new StreamReader ( new WebClient ( ).OpenRead ( URL ) );
			tempHtml = sr.ReadToEnd ( );

			return tempHtml;
		}
		private string getImdbMovieFullCreditsPageHtml ( ) {
			string tempHtml;

			string URL = "http://www.imdb.com/title/" + "tt1315962" + "/fullcredits";
			StreamReader sr = new StreamReader ( new WebClient ( ).OpenRead ( URL ) );
			tempHtml = sr.ReadToEnd ( );

			return tempHtml;
		}

		private MatchCollection GetRegexMatches ( string pattern, string content ) {
			Regex rgx = new Regex ( pattern, 
				RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace );
			return rgx.Matches ( content );
		}

		private Match GetRegexMatch ( string pattern, string content ) {
			Regex rgx = new Regex ( pattern,
				RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace );
			return rgx.Match ( content );
		}

		//[TestMethod]
		//public void ImageParseTest ( ) {
		//	string tempHtml = getImdbMoviePageHtml ( );
		//	string result = "";
		//	string pattern = @"<img [^\>]* Partir. [^\>]* src \s* = \s* [\""\']? ( [^\""\'\s>]* )";

		//	MatchCollection matches = GetRegexMatches ( pattern, tempHtml );
		//	result = matches[0].Groups[1].Value;




		//	HtmlCodes htmlCodes = new HtmlCodes ( );
		//	string _title = "";
		//	string titlePattern = "<title>.*</title>";
		//	Regex rgx;
		//	rgx = new Regex ( titlePattern,
		//		RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace );

		//	if ( rgx.Matches ( tempHtml ).Count > 0 ) {
		//		_title = rgx.Matches ( tempHtml )[0].Value;

		//		//remove the beginning title tag <title>
		//		_title = _title.Substring ( 7 );

		//		//remove the ending title tag </title>
		//		_title = _title.Substring ( 0, _title.Length - 8 );

		//		_title = htmlCodes.ToText ( _title );
		//	}
		//}

		[TestMethod]
		public void ImageParseTest ( ) {
			string tempHtml = getImdbMoviePageHtml ( );
			string result = "";
			string pattern = @"<img [^\>]* Partir. [^\>]* src \s* = \s* [\""\']? ( [^\""\'\s>]* )";

			MatchCollection matches = GetRegexMatches ( pattern, tempHtml );
			result = matches[0].Groups[1].Value;
		}

		[TestMethod]
		public void RatingParseTest ( ) {
			string tempHtml = getImdbMoviePageHtml ( );
			string result = "";
			string pattern = @"<span.*?itemprop.*?ratingValue.*?>(.*?)</.*?span.*?>";

			MatchCollection matches = GetRegexMatches ( pattern, tempHtml );
			
			string temp;
			temp = matches[0].Groups[1].Value;
			CultureInfo ci = System.Globalization.CultureInfo.CurrentCulture;
			temp = temp.Replace ( ".", ci.NumberFormat.CurrencyDecimalSeparator );
			result = double.Parse ( temp ).ToString ( );
		}

		[TestMethod]
		public void YearParseTest ( ) {
			string tempHtml = getImdbMoviePageHtml ( );
			string result = "";
			string pattern = @"<a.*?href.*?/year/([0-9]{4})/.*?</a>";

			MatchCollection matches = GetRegexMatches ( pattern, tempHtml );
			result = int.Parse ( matches[0].Groups[1].Value ).ToString ( );
		}

		[TestMethod]
		public void GenresParseTest ( ) {
			string tempHtml = getImdbMoviePageHtml ( );
			string result = "";
			string pattern1 = @"<div class=""infobar"">.*?</div>";
			string pattern2 = @"<span.*?itemprop=""genre"".*?>(.*?)</span>";

			Regex rgx = new Regex ( pattern1, RegexOptions.IgnoreCase | RegexOptions.Singleline );
			MatchCollection matches = rgx.Matches( tempHtml );

			rgx = new Regex ( pattern2, RegexOptions.IgnoreCase );
			matches = rgx.Matches ( matches[0].Value );

			result = matches[0].Groups[1].Value;
		}

		[TestMethod]
		public void LanguagesParseTest ( ) {
			string tempHtml = getImdbMoviePageHtml ( );
			string result = "";
			string pattern1 = @"id=""titleDetails"".*?Language[s]*(.*?)</div>";
			string pattern2 = @"href=""/language/.*?>(.*?)</a>";

			Regex rgx = new Regex ( pattern1, RegexOptions.IgnoreCase | RegexOptions.Singleline );
			MatchCollection matches = rgx.Matches ( tempHtml );

			rgx = new Regex ( pattern2, RegexOptions.IgnoreCase | RegexOptions.Singleline );
			matches = rgx.Matches ( matches[0].Value );

			result = matches[0].Groups[1].Value;
		}

		//[TestMethod]
		//public void LanguagesParseTest ( ) {
		//	string tempHtml = getImdbMovieFullCreditsPageHtml ( );
		//	string result = "";
		//	string pattern1 = @"id=""titleDetails"".*?Language[s]*(.*?)</div>";
		//	string pattern2 = @"href=""/language/.*?>(.*?)</a>";

		//	Regex rgx = new Regex ( pattern1, RegexOptions.IgnoreCase | RegexOptions.Singleline );
		//	MatchCollection matches = rgx.Matches ( tempHtml );

		//	rgx = new Regex ( pattern2, RegexOptions.IgnoreCase | RegexOptions.Singleline );
		//	matches = rgx.Matches ( matches[0].Value );

		//	result = matches[0].Groups[1].Value;
		//}
	}
}