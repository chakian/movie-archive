using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using com.cagdaskorkut.utility;

namespace MArchiveLibrary {
	public class imdbHelper {
		static TextWriter tw;
		static DateTime startTime;

		#region get movie information with the IMDB ID
		public static imdbModel getMovieInformation ( String imdbId, TextWriter tempTw, DateTime startTemp ) {
			tw = tempTw;
			startTime = startTemp;
			return getMovieInformation ( imdbId );
		}

        private static string GetMoviePageHtml(string imdbId)
        {
            try
            {
                string html = WebRequestHelper.CallPage("http://www.imdb.com/title/" + imdbId);
                return html;
            }
            catch { throw; }
        }
        private static string GetMovieFullCreditsHtml(string imdbId)
        {
            try
            {
                string html = WebRequestHelper.CallPage("http://www.imdb.com/title/" + imdbId + "/fullcredits");

                Regex rgx = new Regex(@"<div.*?id=""fullcredits_content"".*?(.*?)<div id=""sidebar""", RegexOptions.Singleline);
                html = rgx.Matches(html)[0].Groups[1].Value;
                html = html.Replace('\n', ' ');
                return html;
            }
            catch { throw; }
        }

		public static imdbModel getMovieInformation (string imdbId ) {
			imdbModel inf = new imdbModel ( );

			inf.id = imdbId;

			if ( !string.IsNullOrEmpty ( imdbId ) ) {
                string tempHtml = "";

                tempHtml = GetMoviePageHtml(imdbId);

				calculateAndWriteProcessTime ( "Movie page called" );

				if ( tempHtml != "" ) {
                    inf.picturePath = GetPicturePath(tempHtml, imdbId);
					calculateAndWriteProcessTime ( "Picture parsed" );
					
					inf.imdbRating = GetRating(tempHtml, imdbId);
					calculateAndWriteProcessTime ( "Rating parsed" );

                    inf.year = GetYear(tempHtml, imdbId);
					calculateAndWriteProcessTime ( "Year parsed" );
					
                    inf.genres = GetGenres(tempHtml, imdbId);
					calculateAndWriteProcessTime ( "Genres parsed" );
					
                    inf.languages = GetLanguages(tempHtml, imdbId);
					calculateAndWriteProcessTime ( "Languages parsed" );

					//in this part, we will get a new html to parse.. it will be the "fullcredits"
                    tempHtml = GetMovieFullCreditsHtml(imdbId);
					calculateAndWriteProcessTime ( "Full Credits page called" );

					if ( tempHtml != "" ) {
                        inf.directors = GetDirectors(tempHtml, imdbId);
						calculateAndWriteProcessTime ( "Directors parsed" );

                        inf.writers = GetWriters(tempHtml, imdbId);
						calculateAndWriteProcessTime ( "Writers parsed" );

                        inf.cast = GetCast(tempHtml, imdbId);
						calculateAndWriteProcessTime ( "Cast parsed" );
					} else {
						throw new Exception ( "Error while connecting to IMDB for full credits page" );
					}
				} else {
					throw new Exception ( "Error while connecting to IMDB for movie page" );
				}
			}

			return inf;
		}
		#endregion

        private static string GetTitle(string htmlInput, string imdbId)
        {
            HtmlCodes htmlCodes = new HtmlCodes();
            string _title = "";
            string titlePattern = "<title>.*</title>";
            Regex rgx = new Regex(titlePattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

            if (rgx.Matches(htmlInput).Count > 0)
            {
                _title = rgx.Matches(htmlInput)[0].Value;

                //remove the beginning title tag <title>
                _title = _title.Substring(7);

                //remove the ending title tag </title>
                _title = _title.Substring(0, _title.Length - 8);

                _title = htmlCodes.ToText(_title);
            }
            return _title;
        }
        private static string GetPicturePath(string htmlInput, string imdbId)
        {
            string _title = GetTitle(htmlInput, imdbId);
            string _tempTitle = _title.Split(' ')[0];
            string _picturePath;
            Regex rgx = new Regex(@"<img [^\>]* " + _tempTitle + @".*Poster.*[^\>]* src \s* = \s* [\""\']? ( [^\""\'\s>]* )", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            try
            {
                MatchCollection matches = rgx.Matches(htmlInput);
                if (matches.Count > 0)
                    _picturePath = matches[0].Groups[1].Value;
                else
                    _picturePath = null;
            }
            catch
            {
                _picturePath = null;
            }
            return _picturePath;
        }
        private static double? GetRating(string htmlInput, string imdbId)
        {
            double? _rating;

            Regex rgx = new Regex(@"<span.*?itemprop.*?ratingValue.*?>(.*?)</.*?span.*?>", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            try
            {
                string temp = rgx.Matches(htmlInput)[0].Groups[1].Value;
                if (temp == "")
                    _rating = null;
                else
                {
                    CultureInfo ci = System.Globalization.CultureInfo.CurrentCulture;
                    temp = temp.Replace(".", ci.NumberFormat.CurrencyDecimalSeparator);
                    _rating = double.Parse(temp);
                }
            }
            catch
            {
                _rating = null;
            }

            return _rating;
        }
        private static int? GetYear(string htmlInput, string imdbId)
        {
            int? _year;

            Regex rgx = new Regex(@"<a.*?href.*?/year/([0-9]{4})/.*?</a>", RegexOptions.Singleline);
            try
            {
                _year = int.Parse(rgx.Matches(htmlInput)[0].Groups[1].Value);
            }
            catch
            {
                _year = null;
            }

            return _year;
        }
        private static List<string> GetGenres(string htmlInput, string imdbId)
        {
            List<string> _genres;

            Regex rgx = new Regex(@"<div class=""infobar"">.*?</div>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            try
            {
                string temp = rgx.Matches(htmlInput)[0].Value;
                rgx = new Regex(@"<span.*?itemprop=""genre"".*?>(.*?)</span>", RegexOptions.IgnoreCase);
                MatchCollection tempMatchCollection = rgx.Matches(temp);
                _genres = new List<string>();
                foreach (Match item in tempMatchCollection)
                {
                    if (!_genres.Contains(item.Groups[1].Value))
                        _genres.Add(item.Groups[1].Value);
                }
            }
            catch (Exception e)
            {
                _genres = null;
            }

            return _genres;
        }
        private static List<string> GetLanguages(string htmlInput, string imdbId)
        {
            List<string> _languages;

            Regex rgx = new Regex(@"id=""titleDetails"".*?Language[s]*(.*?)</div>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            try
            {
                string temp = rgx.Matches(htmlInput)[0].Value;
                rgx = new Regex(@"href=""/language/.*?>(.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                MatchCollection tempMatchCollection = rgx.Matches(temp);
                _languages = new List<string>();
                foreach (Match item in tempMatchCollection)
                {
                    if (!_languages.Contains(item.Groups[1].Value))
                        _languages.Add(item.Groups[1].Value);
                }
            }
            catch
            {
                _languages = null;
            }

            return _languages;
        }
        private static List<string> GetDirectors(string htmlInput, string imdbId)
        {
            List<string> _directors;

            Regex rgx = new Regex(@"Directed by.*?</h4>.*?<tbody>(.*?)</tbody>", System.Text.RegularExpressions.RegexOptions.Singleline);
            try
            {
                string temp = rgx.Matches(htmlInput)[0].Groups[1].Value;
                rgx = new Regex(@"<tr.*?<a.*?href=""/name/.*?>(.*?)</a>.*?</tr>", RegexOptions.Singleline);
                MatchCollection tempMatchCollection = rgx.Matches(temp);
                _directors = new List<string>();
                foreach (Match item in tempMatchCollection)
                {
                    if (!_directors.Contains(item.Groups[1].Value.Trim()))
                        _directors.Add(item.Groups[1].Value.Trim());
                }
            }
            catch
            {
                _directors = null;
            }

            return _directors;
        }
        private static List<string> GetWriters(string htmlInput, string imdbId)
        {
            List<string> _writers;

            Regex rgx = new Regex(@"Writing Credits.*?</h4>.*?<tbody>(.*?)</tbody>", System.Text.RegularExpressions.RegexOptions.Singleline);
            try
            {
                string temp = rgx.Matches(htmlInput)[0].Groups[1].Value;
                rgx = new Regex(@"<tr.*?<a.*?href=""/name/.*?>(.*?)</a>.*?</tr>", RegexOptions.Singleline);
                MatchCollection tempMatchCollection = rgx.Matches(temp);
                _writers = new List<string>();
                foreach (Match item in tempMatchCollection)
                {
                    if (!_writers.Contains(item.Groups[1].Value.Trim()))
                        _writers.Add(item.Groups[1].Value.Trim());
                }
            }
            catch
            {
                _writers = null;
            }

            return _writers;
        }
        private static List<string> GetCast(string htmlInput, string imdbId)
        {
            List<string> _cast;

            Regex rgx = new Regex(@">\s*Cast.*?</h4>.*?<table(.*?)</table>", RegexOptions.Singleline);
            try
            {
                string temp = rgx.Matches(htmlInput)[0].Groups[1].Value;
                rgx = new Regex(@"<tr.*?<td.*?itemprop=""actor"".*?>.*?<span.*?>(.*?)</span>", RegexOptions.Singleline);
                MatchCollection tempMatchCollection = rgx.Matches(temp);
                _cast = new List<string>();
                foreach (Match item in tempMatchCollection)
                {
                    if (!_cast.Contains(item.Groups[1].Value.Trim()))
                        _cast.Add(item.Groups[1].Value.Trim());
                }
            }
            catch
            {
                _cast = null;
            }

            return _cast;
        }

		private static void calculateAndWriteProcessTime ( string caption ) {
			if ( tw != null ) {
				TimeSpan processTime = DateTime.Now - startTime;
				tw.WriteLine ( caption + " : " + processTime.Seconds.ToString ( ) + "." + processTime.Milliseconds.ToString ( ) + " saniye" );
				startTime = DateTime.Now;
			}
		}

		public static string getSearchFriendlyString ( string keyword ) {
			string searchableKeyword = keyword;

			searchableKeyword = keyword
				.Replace ( ' ', '+' )
				.Replace ( 'ğ', 'g' )
				.Replace ( 'Ğ', 'G' )
				.Replace ( 'ü', 'u' )
				.Replace ( 'Ü', 'U' )
				.Replace ( 'ş', 's' )
				.Replace ( 'Ş', 'S' )
				.Replace ( 'ö', 'o' )
				.Replace ( 'Ö', 'O' )
				.Replace ( 'ç', 'c' )
				.Replace ( 'Ç', 'C' )
				.Replace ( 'ı', 'i' )
				.Replace ( 'İ', 'I' )
				.Replace ( 'á', 'a' )
				.Replace ( 'Á', 'A' )
				.Replace ( 'à', 'a' )
				.Replace ( 'À', 'A' )
				.Replace ( 'é', 'e' )
				.Replace ( 'É', 'E' )
				.Replace ( 'è', 'e' )
				.Replace ( 'È', 'E' );

			return searchableKeyword;
		}
	}
}