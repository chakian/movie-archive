using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MArchive.DataContext;
using MArchive.Domain.Movie;

namespace MArchive.BL {
	public class MovieListBL {
		private static List<MovieListDO> getMovieListDO( List<MOV_M_Movie> list ) {
			List<MovieListDO> movieList = new List<MovieListDO>( );

			foreach( var item in list ) {
				MovieListDO movie = new MovieListDO( );
				movie.Movie = Mapper.Map<MOV_M_Movie, MovieDO>( item );

				string _orgNameLang = item.MOV_M_Names.Single( n => n.IsDefault == true ).INF_Language.Name;
				movie.OriginalName = item.MOV_M_Names.Single( n => n.IsDefault == true ).Name;
				MOV_M_Name _tempName;
				if( _orgNameLang == "English" ) {
					movie.EnglishName = movie.OriginalName;
				} else {
                    _tempName = item.MOV_M_Names.SingleOrDefault(n => n.INF_Language.Name == "English");
					if( _tempName != null )
						movie.EnglishName = _tempName.Name;
				}

				if( _orgNameLang == "Turkish" ) {
					movie.TurkishName = movie.OriginalName;
				} else {
                    _tempName = item.MOV_M_Names.SingleOrDefault(n => n.INF_Language.Name == "Turkish");
					if( _tempName != null )
						movie.TurkishName = _tempName.Name;
				}

				movieList.Add( movie );
			}

			return movieList;
		}
		private static List<MovieListDO> getMovieListDO( List<int> idList ) {
			return getMovieListDO( MovieBL.GetAll( ).Where( q => idList.Contains( q.ID ) ).ToList( ) );
		}
		#region Movie Listing
		public static List<MovieListDO> GetAllForListing( ) {
			var list = MovieBL.GetAll( ).ToList( );
			return getMovieListDO( list );
		}
		public static List<MovieListDO> GetAllInDiscsForListing( ) {
			var list = MovieBL.GetAll( ).Where( q => q.MOV_M_Archives.Any( a => a.MovieID == q.ID ) ).ToList( );
			return getMovieListDO( list );
		}
		public static List<MovieListDO> GetThisYearForListing( ) {
			var list = MovieBL.GetAll( ).Where( q => q.Year.HasValue && q.Year == DateTime.Now.Year ).ToList( );
			return getMovieListDO( list );
		}
		public static List<MovieListDO> GetLastYearForListing( ) {
			var list = MovieBL.GetAll( ).Where( q => q.Year.HasValue && q.Year == DateTime.Now.Year - 1 ).ToList( );
			return getMovieListDO( list );
		}
		#endregion Movie Listing
		#region Searching
		public static List<MovieListDO> SearchSuperblyForNames( string keyword ) {
			//search in movie names
			List<int> ids = MovieNameBL.SearchDOByLikelyName( keyword ).Select( q => q.MovieID ).ToList( );
			return getMovieListDO( ids );
		}
		public static List<MovieListDO> SearchSuperblyForActors( string keyword ) {
			//search in actors
			List<int> ids = MovieNameBL.SearchDOByLikelyName( keyword ).Select( q => q.MovieID ).ToList( );
			return getMovieListDO( ids );
		}
		public static List<MovieListDO> SearchSuperblyForDirectors( string keyword ) {
			//search in directors
			List<int> ids = MovieNameBL.SearchDOByLikelyName( keyword ).Select( q => q.MovieID ).ToList( );
			return getMovieListDO( ids );
		}
		public static List<MovieListDO> SearchSuperblyForWriters( string keyword ) {
			//search in writers
			List<int> ids = MovieNameBL.SearchDOByLikelyName( keyword ).Select( q => q.MovieID ).ToList( );
			return getMovieListDO( ids );
		}
		public static List<MovieListDO> SearchSuperblyForTypes( string keyword ) {
			//search in movie types
			List<int> ids = MovieNameBL.SearchDOByLikelyName( keyword ).Select( q => q.MovieID ).ToList( );
			return getMovieListDO( ids );
		}
		public static List<MovieListDO> SearchSuperblyForLanguages( string keyword ) {
			//search in languages
			List<int> ids = MovieNameBL.SearchDOByLikelyName( keyword ).Select( q => q.MovieID ).ToList( );
			return getMovieListDO( ids );
		}
		#endregion Searching
	}
}