using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using MArchive.BL;
using MArchive.Domain.Movie;
using MArchiveLibrary;
using com.cagdaskorkut.utility;

namespace MArchiveImdbParser {
	public partial class Form1 : Form, IPicture {
		List<MovieDO> mListCurrent = new List<MovieDO>( );
		List<MovieDO> mListWaiting = new List<MovieDO>( );
		int mCount = 0;

		//List<MovieActorDO> allActors;
		Dictionary<string, int> allActors;
		Dictionary<string, int> allDirectors;
		Dictionary<string, int> allLanguages;
		Dictionary<string, int> allTypes;
		Dictionary<string, int> allWriters;

		public Form1( ) {
			InitializeComponent( );
		}

		private void InitializeDictionaries( ) {
			allActors = new Dictionary<string, int>( );
			foreach( var item in ActorBL.GetAllDO( ) ) {
				allActors.Add( item.Name, item.ID );
				setProgressBarValue( parseStatus, parseStatus.Value + 1 );
			}

			allDirectors = new Dictionary<string, int>( );
			foreach( var item in DirectorBL.GetAllDO( ) ) {
				allDirectors.Add( item.Name, item.ID );
				setProgressBarValue( parseStatus, parseStatus.Value + 1 );
			}

			allLanguages = new Dictionary<string, int>( );
			foreach( var item in LanguageBL.GetAllDO( ) ) {
				allLanguages.Add( item.Name, item.ID );
				setProgressBarValue( parseStatus, parseStatus.Value + 1 );
			}

			allTypes = new Dictionary<string, int>( );
			foreach( var item in TypeBL.GetAllDO( ) ) {
				allTypes.Add( item.Name, item.ID );
				setProgressBarValue( parseStatus, parseStatus.Value + 1 );
			}

			allWriters = new Dictionary<string, int>( );
			foreach( var item in WriterBL.GetAllDO( ) ) {
				allWriters.Add( item.Name, item.ID );
				setProgressBarValue( parseStatus, parseStatus.Value + 1 );
			}
		}

		private void Form1_Load( object sender, EventArgs e ) {
			BLInitializer.Initialize( );

			parseStatus.Minimum = 0;
			parseStatus.Maximum = ActorBL.GetAll( ).Count( )
				+ DirectorBL.GetAll( ).Count( )
				+ LanguageBL.GetAll( ).Count( )
				+ TypeBL.GetAll( ).Count( )
				+ WriterBL.GetAll( ).Count( );
			parseStatus.Value = 0;

			Thread loadDictionaryThread = new Thread( new ThreadStart( InitializeDictionaries ) );
			loadDictionaryThread.Start( );

            //txtImdbId.Text = "tt0243493"; //the closet
            //txtImdbId.Text = "tt0361748"; //ingl. basterds - multi director

			//txtSearch.Text = "Ocean's Eleven";
		}
		#region get Test
		private void btnGetByImdbId_Click( object sender, EventArgs e ) {
			//IMDB ID
			if( !string.IsNullOrEmpty( txtImdbId.Text ) ) {
				string imdbId = txtImdbId.Text;

				string logPath = @"D:\Development\Projects\MArchive\MArchiveImdbParser\_log";

				Directory.CreateDirectory( logPath );

				TextWriter tw = new StreamWriter( logPath + logHelper.createLogFileName(string.Format( "{0}", imdbId ) ) );
				DateTime start = DateTime.Now;
				tw.WriteLine( "Start : " + start.ToString( ) );

				ImdbModel iInfo = new ImdbModel( );
				try {
					iInfo = ImdbHelper.getMovieInformation( imdbId, tw, start );
					DateTime finish = DateTime.Now;
					TimeSpan processTime = finish - start;
					imdbInfoForm frmImdbInfo = new imdbInfoForm( iInfo, this, "addnew" );
					//frmImdbInfo.MdiParent = this.MdiParent;
					frmImdbInfo.Text += "  - " + processTime.Seconds.ToString( ) + "." + processTime.Milliseconds.ToString( ) + " saniye";
					frmImdbInfo.Show( );
				} catch( Exception ex ) { MessageBox.Show( ex.Message ); }

				tw.WriteLine( "End : " + DateTime.Now.ToString( ) );
				tw.Close( );
			}
		}
		#endregion
		#region search Test
		private void btnSearch_Click( object sender, EventArgs e ) {
			//MOVIE NAME
			if( !string.IsNullOrEmpty( txtSearch.Text ) ) {
				string search = ImdbHelper.getSearchFriendlyString( txtSearch.Text );
				string imdbId = "";
                string tempHtml = "";
				//StreamReader sr = new StreamReader ( @"E:\Projects\MArchive\MArchiveImdbParser\imdb_se.txt" );
				//tempHtml = sr.ReadToEnd ( );
				try {
					tempHtml = WebRequestHelper.CallPage( "http://www.imdb.com/find?tt=all&q=" + search.Replace( " ", "+" ) );
				} catch( Exception ex ) { MessageBox.Show( ex.Message ); }

				Regex rgx;
				MatchCollection matches;

				if( tempHtml != "" ) {
					string temp = "";

					//check if this is the search page
					//or if we've been redirected to the movie's page
					string _searchPagePattern = "<link.*href.*imdb.com/find";

					rgx = new System.Text.RegularExpressions.Regex( _searchPagePattern );
					matches = rgx.Matches( tempHtml );

					if( matches.Count > 0 ) {
						//arama sayfasına gelmişiz

						_searchPagePattern = string.Format( "<div class={0}{1}findSection{0}{1}>.*<table class={0}{1}findList{0}{1}.*>(.*)</table>.*More title matches",
							"\\", "\"" );
						rgx = new Regex( _searchPagePattern, RegexOptions.Singleline );
						matches = rgx.Matches( tempHtml );

						if( matches.Count > 0 ) {
							string tableContent = matches[0].Groups[1].Value;

							_searchPagePattern = string.Format( "<a href=\"/title/(tt[0-9]{7,9})/" );
							//TODO: Burada kaldım
						}

						if( tempHtml.Contains( "<b>Popular Titles</b>" ) ) {
							//popüler sonuçlarda bulduk, onu alalım
							temp = tempHtml.Substring( tempHtml.IndexOf( "<b>Popular Titles</b>" ) );
							temp = temp.Substring( temp.IndexOf( "<a href=\"/title/tt" ) + 16 );
							imdbId = temp.Remove( temp.IndexOf( "/" ) );
						} else {
							//popülerde yoksa, şimdilik salla, sonra bakalım ama - - -!!!
							throw new Exception( "I could not find the movie that you searched for.. Sorry." );
						}
					} else {
						//sayfayı bulmuşuz
						if( tempHtml.Contains( "<link href=\"http://www.imdb.com/title/" ) ) {
							temp = tempHtml.Substring( tempHtml.IndexOf( "<link href=\"http://www.imdb.com/title/" ) + 38 );
						} else if( tempHtml.Contains( "<link rel=\"canonical\" href=\"http://www.imdb.com/title/" ) ) {
							temp = tempHtml.Substring( tempHtml.IndexOf( "<link rel=\"canonical\" href=\"http://www.imdb.com/title/" ) + 54 );
						}
						imdbId = temp.Remove( temp.IndexOf( "/" ) );
					}
				} else {
					throw new Exception( "Error while connecting to IMDB" );
				}

				MessageBox.Show( imdbId );
			}
		}
		#endregion

		private void btnStart_Click( object sender, EventArgs e ) {
			//MOVIE LIST THINGS
			mListCurrent = MovieBL.GetAllImdbless( chkCheckPosterless.Checked ).ToList( );

			//mListCurrent = new List<MovieDO> ( );
			//mListCurrent.Add ( MovieBL.GetMovie ( 1054 ) );
			//int howManyDoYouWant = 1;
			//mListCurrent.RemoveRange( howManyDoYouWant, mListCurrent.Count - howManyDoYouWant );

			mListWaiting = new List<MovieDO>( );

			//MOVIE COUNT THINGS
			mCount = mListCurrent.Count;
			parseStatus.Minimum = 0;
			parseStatus.Maximum = mCount;
			parseStatus.Value = 0;

			//START THREAD
			Thread parseThread = new Thread( new ThreadStart( parseMovies ) );
			parseThread.Start( );

			setButtonEnabled( btnStart, false );
		}

		private void parseMovies( ) {
			string logPath = string.Format( Application.StartupPath + @"\_log\{0}{1}{2}_{3}_{4}_{5}\",
						DateTime.Now.Year.ToString( ),
						DateTime.Now.Month.ToString( ).PadLeft( 2, '0' ),
						DateTime.Now.Day.ToString( ).PadLeft( 2, '0' ),
						DateTime.Now.Hour.ToString( ).PadLeft( 2, '0' ),
						DateTime.Now.Minute.ToString( ).PadLeft( 2, '0' ),
						DateTime.Now.Second.ToString( ).PadLeft( 2, '0' ) );

			Directory.CreateDirectory( logPath );

			TextWriter twSum = new StreamWriter( logPath + logHelper.createLogFileName( "_summary" ) );
			twSum.WriteLine(string.Format( "{0}\tThread started for {1} movies",
				DateTime.Now.ToString( ),
				mCount.ToString( ) ) );

			foreach( var item in mListCurrent ) {
				string movieName = MovieNameBL.GetOriginalNameOfMovie( item.ID );
				if( !string.IsNullOrEmpty( item.ImdbID ) ) {
					string imdbId = item.ImdbID;

					TextWriter tw = new StreamWriter( logPath + logHelper.createLogFileName( String.Format( "{0} ({1})", movieName, imdbId ) ) );
					DateTime start = DateTime.Now;
					//TODO: Stopwatch olarak geliştirirsek daha bi güzel olur aslında
					Stopwatch sw = new Stopwatch( );
					sw.Start( );
					tw.WriteLine( " GET BY ID " );
					tw.WriteLine( "Start : " + start.ToString( ) );

					getMovieInfoByIdAndSave( imdbId, item, tw, start );

					TimeSpan processTime = sw.Elapsed;
					sw.Stop( );

					tw.WriteLine( "End : " + DateTime.Now.ToString( ) );
					tw.WriteLine( );
					tw.WriteLine( String.Format( "Total elapsed time: {0} minutes {1} seconds {2} miliseconds",
						processTime.Minutes.ToString( ),
						processTime.Seconds.ToString( ),
						processTime.Milliseconds.ToString( ) ) );
					tw.Close( );
				} else if( !String.IsNullOrEmpty( movieName ) ) {
					bool skipSearchForNow = true;
					if( skipSearchForNow == false ) {
						string search = movieName;
						string imdbId = "";
						String tempHtml = "";

						TextWriter tw = new StreamWriter( logPath + logHelper.createLogFileName( String.Format( "{0}", movieName ) ) );
						DateTime start = DateTime.Now;
						Stopwatch sw = new Stopwatch( );
						sw.Start( );
						logHelper.logLine( tw, " GET BY SEARCH " );
						logHelper.logLine( tw, "Search started" );

						try {
							tempHtml = WebRequestHelper.CallPage( "http://www.imdb.com/find?tt=all&q=" + search.Replace( " ", "+" ) );
						} catch( Exception ex ) { logHelper.logException( tw, ex ); }

						logHelper.logLine( tw, "Query ran successfully" );

						System.Text.RegularExpressions.Regex rgx;

						if( tempHtml != "" ) {
							string temp = "";

							//check if this is the search page
							//or if we've been redirected to the movie's page
							rgx = new System.Text.RegularExpressions.Regex( @"<title>IMDb Title Search</title>" );

							if( rgx.Matches( tempHtml ).Count == 1 ) {
								//arama sayfasına gelmişiz

								logHelper.logLine( tw, "We are at search result page" );

								//we'll parse all movies in this page and show the user the list to select from
								//if there is only 1 exact title result, we will take it as the single result
								if( tempHtml.Contains( "<b>Popular Titles</b>" ) ) {
									logHelper.logLine( tw, "Our movie is in the Popular Titles!" );

									//popüler sonuçlarda bulduk, onu alalım
									temp = tempHtml.Substring( tempHtml.IndexOf( "<b>Popular Titles</b>" ) );
									temp = temp.Substring( temp.IndexOf( "<a href=\"/title/tt" ) + 16 );
									imdbId = temp.Remove( temp.IndexOf( "/" ) );
								} else {
									logHelper.logLine( tw, "Movie is not so Popular. Not checking now. Sent to waiting queue." );
									//popülerde yoksa, şimdilik salla, sonra bakalım ama - - -!!!
									copyItemToList( mListCurrent, mListWaiting, item.ID );
								}
							} else {
								//sayfayı bulmuşuz

								logHelper.logLine( tw, "We are at the movie page" );

								if( tempHtml.Contains( "<link href=\"http://www.imdb.com/title/" ) ) {
									temp = tempHtml.Substring( tempHtml.IndexOf( "<link href=\"http://www.imdb.com/title/" ) + 38 );
								} else if( tempHtml.Contains( "<link rel=\"canonical\" href=\"http://www.imdb.com/title/" ) ) {
									temp = tempHtml.Substring( tempHtml.IndexOf( "<link rel=\"canonical\" href=\"http://www.imdb.com/title/" ) + 54 );
								}
								imdbId = temp.Remove( temp.IndexOf( "/" ) );
							}
							if( !String.IsNullOrEmpty( imdbId ) ) {
								logHelper.logLine( tw, "Got the ID: " + imdbId );
								getMovieInfoByIdAndSave( imdbId, item, tw, start );
							}
						} else {
							logHelper.logLine( tw, "Error while connecting to IMDB. Aborting." );

							copyItemToList( mListCurrent, mListWaiting, item.ID );
						}

						tw.Close( );
					}
				} else {
					//movie is fucked up. forget about it..
					//removeItemFromMovieList ( mListCurrent, item.movieId );
				}

				setProgressBarValue( parseStatus, parseStatus.Value + 1 );
			}
			twSum.WriteLine(string.Format( "{0}\tThread finished with {1} movies waiting in queue",
				DateTime.Now.ToString( ),
				mListWaiting.Count.ToString( ) ) );
			twSum.Close( );

			MessageBox.Show(string.Format( "{0}\tThread finished with {1} movies waiting in queue",
				DateTime.Now.ToString( ),
				mListWaiting.Count.ToString( ) ) );

			setButtonEnabled( btnStart, true );
		}

		#region thread helper functions
		delegate void SetProgressBarCallback( ProgressBar pb, int value );
		void setProgressBarValue( ProgressBar pb, int value ) {
			if( pb.InvokeRequired ) {
				SetProgressBarCallback d = new SetProgressBarCallback( setProgressBarValue );
				this.Invoke( d, new object[] { pb, value } );
			} else {
				pb.Value = value;
			}

			string val = ( ( Double )( ( Double )pb.Value ) / ( ( Double )pb.Maximum ) * 100 ).ToString( "0.##" ) + " %";
			setProgressTextValue( lblProgress, val );
		}
		delegate void SetProgressTextCallback( Label lbl, string value );
		void setProgressTextValue( Label lbl, string value ) {
			if( lbl.InvokeRequired ) {
				SetProgressTextCallback d = new SetProgressTextCallback( setProgressTextValue );
				this.Invoke( d, new object[] { lbl, value } );
			} else {
				lbl.Text = value;
			}
		}
		delegate void SetButtonEnabledCallback( Button btn, bool value );
		void setButtonEnabled( Button btn, bool value ) {
			if( btn.InvokeRequired ) {
				SetButtonEnabledCallback d = new SetButtonEnabledCallback( setButtonEnabled );
				this.Invoke( d, new object[] { btn, value } );
			} else {
				btn.Enabled = value;
			}
		}
		#endregion thread helper functions

		private void getMovieInfoByIdAndSave( string imdbId, MovieDO movie, TextWriter tw, DateTime start ) {
			ImdbModel imdbInfo = new ImdbModel( );
			try {
				imdbInfo = ImdbHelper.getMovieInformation( imdbId, tw, start );

				logHelper.logLine( tw, "Imdb information parsed successfully. Starting operation 'Basic Info'" );

				#region Basic Info (not updated in db yet)
				if(string.IsNullOrEmpty( movie.ImdbID ) ) movie.ImdbID = imdbId;
				if(string.IsNullOrEmpty( movie.ImdbPoster ) && !string.IsNullOrEmpty( imdbInfo.picturePath ) ) {
					string savePath = getSavePath( );
					string fileName = FileSystemHelper.prepareFileNameForPicture( imdbInfo.picturePath.Substring( imdbInfo.picturePath.LastIndexOf( '/' ) ), movie, savePath );
                    WebRequestHelper.Download(imdbInfo.picturePath, savePath + fileName);
					movie.ImdbPoster = fileName;
				}
				
				movie.ImdbRating = imdbInfo.imdbRating;
				if( movie.Year == null || movie.Year == 0 ) movie.Year = imdbInfo.year;
				#endregion Basic Info

				logHelper.logLine( tw, "Basic Info inserted" );

				logHelper.logLine( tw, "Starting operation 'Genres'" );

				#region types
				if( MovieTypeBL.GetAllByMovieID( movie.ID ).Count() < 1 ) {
					try {
						if( imdbInfo.genres != null ) {
							foreach( string item in imdbInfo.genres ) {
								if( allTypes.ContainsKey( item ) ) {
									MovieTypeBL.Save( new MovieTypeDO( ) { MovieID = movie.ID, TypeID = allTypes[item] }, 1 );
								} else {
									int currentID = TypeBL.Save( new MArchive.Domain.Lookup.TypeDO( ) { Name = item }, 1 ).ID;
									//MovieTypeBL.Save( movie.ID, item, 1 );
									MovieTypeBL.Save( new MovieTypeDO( ) { MovieID = movie.ID, TypeID = currentID }, 1 );
									allTypes.Add( item, currentID );
								}
							}
						}
						logHelper.logLine( tw, "Genres inserted" );
					} catch( Exception ex ) {
						logHelper.logLine( tw, "Genres could not be inserted." );
						logHelper.logException( tw, ex );
						logHelper.logLine( tw );
					}
				}
				#endregion types

				logHelper.logLine( tw, "Starting operation 'Actors'" );

				#region actors
				if( MovieActorBL.GetAllByMovieID( movie.ID ).Count( ) < 1 ) {
					try {
						if( imdbInfo.cast != null ) {
							foreach( string item in imdbInfo.cast ) {
								if( allActors.ContainsKey( item ) ) {
									MovieActorBL.Save( new MovieActorDO( ) { MovieID = movie.ID, ActorID = allActors[item] }, 1 );
								} else {
									int currentID = ActorBL.Save( new MArchive.Domain.Lookup.ActorDO( ) { Name = item }, 1 ).ID;
									MovieActorBL.Save( new MovieActorDO( ) { MovieID = movie.ID, ActorID = currentID }, 1 );
									allActors.Add( item, currentID );
								}
							}
						}
						logHelper.logLine( tw, "Actors inserted" );
					} catch( Exception ex ) {
						logHelper.logLine( tw, "Actors could not be inserted." );
						logHelper.logException( tw, ex );
						logHelper.logLine( tw );
					}
				}
				#endregion actors

				logHelper.logLine( tw, "Starting operation 'Languages'" );

				#region languages
				if( MovieLanguageBL.GetAllByMovieID( movie.ID ).Count( ) < 1 ) {
					try {
						if( imdbInfo.languages != null ) {
							foreach( string item in imdbInfo.languages ) {
								if( allLanguages.ContainsKey( item ) ) {
									MovieLanguageBL.Save( new MovieLanguageDO( ) { MovieID = movie.ID, LanguageID = allLanguages[item] }, 1 );
								} else {
									int currentID = LanguageBL.Save( new MArchive.Domain.Lookup.LanguageDO( ) { Name = item }, 1 ).ID;
									MovieLanguageBL.Save( new MovieLanguageDO( ) { MovieID = movie.ID, LanguageID = currentID }, 1 );
									allLanguages.Add( item, currentID );
								}
							}
						}
						logHelper.logLine( tw, "Languages inserted" );
					} catch( Exception ex ) {
						logHelper.logLine( tw, "Languages could not be inserted." );
						logHelper.logException( tw, ex );
						logHelper.logLine( tw );
					}
				}
				#endregion languages

				logHelper.logLine( tw, "Starting operation 'Directors'" );

				#region directors
				if( MovieDirectorBL.GetAllByMovieID( movie.ID ).Count( ) < 1 ) {
					try {
						if( imdbInfo.directors != null ) {
							foreach( string item in imdbInfo.directors ) {
								if( allDirectors.ContainsKey( item ) ) {
									MovieDirectorBL.Save( new MovieDirectorDO( ) { MovieID = movie.ID, DirectorID = allDirectors[item] }, 1 );
								} else {
									int currentID = DirectorBL.Save( new MArchive.Domain.Lookup.DirectorDO( ) { Name = item }, 1 ).ID;
									MovieDirectorBL.Save( new MovieDirectorDO( ) { MovieID = movie.ID, DirectorID = currentID }, 1 );
									allDirectors.Add( item, currentID );
								}
							}
						}
						logHelper.logLine( tw, "Directors inserted" );
					} catch( Exception ex ) {
						logHelper.logLine( tw, "Directors could not be inserted." );
						logHelper.logException( tw, ex );
						logHelper.logLine( tw );
					}
				}
				#endregion directors

				logHelper.logLine( tw, "Starting operation 'Writers'" );

				#region writers
				if( MovieWriterBL.GetAllByMovieID( movie.ID ).Count( ) < 1 ) {
					try {
						if( imdbInfo.writers != null ) {
							foreach( string item in imdbInfo.writers ) {
								if( allWriters.ContainsKey( item ) ) {
									MovieWriterBL.Save( new MovieWriterDO( ) { MovieID = movie.ID, WriterID = allWriters[item] }, 1 );
								} else {
									int currentID = WriterBL.Save( new MArchive.Domain.Lookup.WriterDO( ) { Name = item }, 1 ).ID;
									MovieWriterBL.Save( new MovieWriterDO( ) { MovieID = movie.ID, WriterID = currentID }, 1 );
									allWriters.Add( item, currentID );
								}
							}
						}
						logHelper.logLine( tw, "Writers inserted" );
					} catch( Exception ex ) {
						logHelper.logLine( tw, "Writers could not be inserted." );
						logHelper.logException( tw, ex );
						logHelper.logLine( tw );
					}
				}
				#endregion writers

				logHelper.logLine( tw, "Updating 'movie' table" );

				movie.ImdbParsed = true;
				movie.ImdbLastParseDate = movie.UpdateDate = DateTime.Now;

				MovieBL.Save( movie, 1 );

				logHelper.logLine( tw, "Table update successful" );

			} catch( Exception ex ) { logHelper.logException( tw, ex ); }
		}

		public string getSavePath( ) {
			return FileSystemHelper.getMoviePictureSavePath( );
		}

		#region List Operations
		private void removeItemFromMovieList( List<MovieDO> mLst, int mId ) {
			mLst.RemoveAll( i => i.ID == mId );
		}
		private void moveItemToList( List<MovieDO> src, List<MovieDO> dst, int mId ) {
			copyItemToList( src, dst, mId );
			removeItemFromMovieList( src, mId );
		}
		private void copyItemToList( List<MovieDO> src, List<MovieDO> dst, int mId ) {
			MovieDO itemToCopy = src.Find( i => i.ID == mId );
			dst.Add( itemToCopy );
		}
		#endregion List Operations
	}
}