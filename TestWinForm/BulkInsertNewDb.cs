using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using com.cagdaskorkut.utility.Security;
using MArchiveLibrary;

namespace TestWinForm {
	public partial class BulkInsertNewDb : Form {
		public BulkInsertNewDb( ) {
			InitializeComponent( );
		}

		private void button1_Click( object sender, EventArgs e ) {
			List<string> nameScript = new List<string>( );
			List<string> ratingScript = new List<string>( );
			List<string> movieScript = new List<string>( );

			movieData data = new movieData( );
			List<tblMovie> movies = data.getMovies( );

			foreach( var item in movies ) {
				//name
				string or, en, tu;
				or = item.movieOriginalName;
				en = item.movieEnglishName;
				tu = item.movieTurkishName;

				if( or == en )
					en = string.Empty;
				if( or == tu )
					tu = string.Empty;

				if( string.IsNullOrEmpty( or ) == false )
					nameScript.Add( @"INSERT INTO [MOV].[Names]([MovieID],[LanguageID],[Name],[IsDefault]) VALUES (" + item.movieId.ToString( ) + ", 0, '" + or.Replace( "'", "''" ) + "', 1)" );
				if( string.IsNullOrEmpty( en ) == false )
					nameScript.Add( @"INSERT INTO [MOV].[Names]([MovieID],[LanguageID],[Name],[IsDefault]) VALUES (" + item.movieId.ToString( ) + ", 1, '" + en.Replace( "'", "''" ) + "', 0)" );
				if( string.IsNullOrEmpty( tu ) == false )
					nameScript.Add( @"INSERT INTO [MOV].[Names]([MovieID],[LanguageID],[Name],[IsDefault]) VALUES (" + item.movieId.ToString( ) + ", 56, '" + tu.Replace( "'", "''" ) + "', 0)" );
				nameScript.Add( "" );

				//rating
				if( item.movieMyRating > 0 ) {
					ratingScript.Add( @"INSERT INTO [MOV].[UserRatings]([UserID],[MovieID],[Watched],[Rating])
VALUES (1, " + item.movieId.ToString( ) + ", 1, '" + item.movieMyRating.ToString( ) + "')" );
				}

				//movie itself
				movieScript.Add( @"INSERT INTO [MOV].[Movies]([ID],[Year],[ImdbID],[ImdbRating],[ImdbPoster],[ImdbParsed],[ImdbLastParseDate],[InsertDate],[UpdateDate])
VALUES (" + item.movieId.ToString( ) + @",
" + ( item.movieYear.HasValue ? item.movieYear.Value.ToString( ) : "NULL" ) + @",
" + ( string.IsNullOrEmpty( item.movieImdbId ) ? "NULL" : "'" + item.movieImdbId + "'" ) + @",
" + ( item.movieImdbRating.HasValue ? "'" + item.movieImdbRating.ToString( ).Replace( ",", "." ) + "'" : "NULL" ) + @",
" + ( string.IsNullOrEmpty( item.movieImdbPoster ) ? "NULL" : "'" + item.movieImdbPoster.Replace( "'", "''" ) + "'" ) + @",
" + ( ( item.imdbParsed.HasValue && item.imdbParsed.Value ) ? "1" : "0" ) + @",
" + ( ( item.imdbParsed.HasValue && item.imdbParsed.Value ) ? "'" + item.updateDate.Value.ToString( "yyyy-MM-dd hh:mm:ss" ) + "'" : "NULL" ) + @",
'" + item.insertDate.Value.ToString( "yyyy-MM-dd hh:mm:ss" ) + @"',
'" + item.updateDate.Value.ToString( "yyyy-MM-dd hh:mm:ss" ) + "')" );
			}


			using( TextWriter tw = new StreamWriter( "Movie.sql" ) ) {
				foreach( string item in movieScript ) {
					tw.WriteLine( item );
				}
				tw.Close( );
			}

			using( TextWriter tw = new StreamWriter( "Name.sql" ) ) {
				foreach( string item in nameScript ) {
					tw.WriteLine( item );
				}
				tw.Close( );
			}

			using( TextWriter tw = new StreamWriter( "Rating.sql" ) ) {
				foreach( string item in ratingScript ) {
					tw.WriteLine( item );
				}
				tw.Close( );
			}
		}

		private void BulkInsertNewDb_Load( object sender, EventArgs e ) {
			textBox1.Text = Encryption.Encrypt( "deneme" );
		}
	}
}
