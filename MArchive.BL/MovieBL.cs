using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using com.cagdaskorkut.utility.CacheUtil;
using com.cagdaskorkut.utility.ExtendedDataContext;
using com.cagdaskorkut.utility.ObjectMapping;
using com.cagdaskorkut.utility.Repository;
using MArchive.DataContext;
using MArchive.Domain.Movie;

namespace MArchive.BL {
    public class MovieBL : BLBase {
        private const string CacheAreaKey = "MArchive_Movie";

		public static MovieDO GetMovie( int id ) {
			return GetAllAsDO( ).Single( x => x.ID == id );
		}

		public static MovieDetailDO GetMovieWithDetails( int id, int currentUserID ) {
			MovieDetailDO movieDetail = new MovieDetailDO( );

			//movie itself
			MOV_M_Movie movie = GetAll( ).Single( m => m.ID == id );
			movieDetail.Movie = Mapper.Map<MOV_M_Movie, MovieDO>( movie );

			//names
			movieDetail.Names = MovieNameBL.GetAllDOByMovieID( id );

			//archives
            movieDetail.Archives = MovieUserArchiveBL.GetAllDOByMovieIDAndUserID(id, currentUserID);

			//languages
			movieDetail.Languages = MovieLanguageBL.GetAllDOByMovieID( id );

			//types
			movieDetail.Types = MovieTypeBL.GetAllDOByMovieID( id );

			//directors
			movieDetail.Directors = MovieDirectorBL.GetAllDOByMovieID( id );

			//writers
			movieDetail.Writers = MovieWriterBL.GetAllDOByMovieID( id );

			//actors
			movieDetail.Actors = MovieActorBL.GetAllDOByMovieID( id );

			//userRatings
			movieDetail.UserRatings = MovieUserRatingBL.GetAllDOByMovieID( id );

			//average user rating
			movieDetail.AverageUserRating = MovieUserRatingBL.GetAverageUserRatingForMovie( id );

			//current user's rating
			movieDetail.CurrentUserRating = MovieUserRatingBL.GetUsersUserRatingForMovie( id, currentUserID );

            //lists
            movieDetail.UserListsIncludingThisMovie = UserListBL.GetAllViewForUserThatIncludeCurrentMovie(id, currentUserID);
            var listIdsThatContainThisMovie = movieDetail.UserListsIncludingThisMovie.Select(q => q.ListID).ToList();
            movieDetail.UserLists = UserListBL.GetAllForUser(currentUserID);
            movieDetail.UserLists.RemoveAll(q => listIdsThatContainThisMovie.Contains(q.ID));

			return movieDetail;
		}

		public static List<MovieDO> GetAllImdbless( bool includePictureless ) {
			return GetAllAsDO( ).Where( q => q.ImdbParsed == false
				|| ( includePictureless == true && string.IsNullOrEmpty( q.ImdbPoster ) ) ).ToList( );
		}

		public static List<MovieDO> GetAllAsDO( ) {
			var list = GetAll( );
			var movieList = Mapper.Map<List<MOV_M_Movie>, List<MovieDO>>( list );
			foreach( var item in movieList ) {
				item.OriginalName = MovieNameBL.GetOriginalNameOfMovie( item.ID );
			}
			return movieList;
		}

        internal static List<MOV_M_Movie> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<MOV_M_Movie>>(CacheAreaKey, functionName) != null) { return ReadCache<List<MOV_M_Movie>>(CacheAreaKey, functionName); }

			Repository<MOV_M_Movie> rep = new Repository<MOV_M_Movie>( MArchiveDataContextProvider.Instance );
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
		}

		public static int Save( MovieDO movieDO, int userID ) {
			Repository<MOV_M_Movie> rep = new Repository<MOV_M_Movie>( MArchiveDataContextProvider.Instance );

			MOV_M_Movie movieToAdd = null;
			if( movieDO.ID == 0 ) {
				movieToAdd = new MOV_M_Movie( );
				ObjectMapper.MapObjects( movieDO, movieToAdd, AuditInfo.Fields );
				rep.InsertOnSubmit( movieToAdd );
			} else {
				movieToAdd = rep.GetAll( ).Single( x => x.ID == movieDO.ID );
				ObjectMapper.MapObjects( movieDO, movieToAdd, AuditInfo.Fields );
			}
            rep.DCP.CommitChanges(userID);

            InvalidateCache(CacheAreaKey);

			ObjectMapper.MapObjects( movieToAdd, movieDO );
			return movieDO.ID;
		}

        public static void ResetImdbInformation(int movieID) {
            MovieArchiveDBDataContext context = new MovieArchiveDBDataContext();
            context.ResetImdbId(movieID);

            InvalidateCache(CacheAreaKey);
        }
    }
}