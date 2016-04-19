using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MArchive.DataContext;
using MArchive.Domain.Movie;

namespace MArchive.BL {
    public class MovieUserRatingBL : BLBase {
        private const string CacheAreaKey = "MArchive_MovieUserRating";

		internal static List<MovieUserRatingDO> GetAllDOByMovieID ( int movieID ) {
			List<MovieUserRatingDO> userRatings = new List<MovieUserRatingDO> ( );
			foreach ( var item in GetAllByMovieID ( movieID ) ) {
                MovieUserRatingDO newItem = Mapper.Map<vMovieUserRating, MovieUserRatingDO>(item);
				userRatings.Add ( newItem );
			}
			return userRatings;
		}

        public static List<MovieUserRatingDO> GetAllDOByUserID(int userID) {
            List<MovieUserRatingDO> userRatings = new List<MovieUserRatingDO>();
            foreach (var item in GetAllByUserID(userID)) {
                MovieUserRatingDO newItem = Mapper.Map<vMovieUserRating, MovieUserRatingDO>(item);
                userRatings.Add(newItem);
            }
            return userRatings;
        }

		internal static double GetAverageUserRatingForMovie ( int movieID ) {
			var list = GetAllDOByMovieID ( movieID );
			if ( list.Where ( r => r.Rating > -1 ).Count ( ) > 0 )
				return list.Where ( r => r.Rating > -1 ).Average ( r => r.Rating );
			else
				return -1;
		}

		internal static MovieUserRatingDO GetUsersUserRatingForMovie ( int movieID, int userID ) {
			var list = GetAllDOByMovieID ( movieID );
			if ( userID > 0 ) {
				var rating = list.SingleOrDefault ( r => r.UserID == userID );
				if ( rating == null ) {
					rating = new MovieUserRatingDO ( );
					rating.MovieID = movieID;
					rating.UserID = userID;
				}
				return rating;
			} else {
                return new MovieUserRatingDO() { UserID = userID };
			}
		}

        private static List<vMovieUserRating> GetAllByMovieID(int movieID)
        {
			return GetAll ( ).Where ( q => q.MovieID == movieID ).ToList ( );
		}
        
        internal static List<vMovieUserRating> GetAllByUserID(int userID)
        {
            return GetAll().Where(q => q.UserID == userID).ToList();
        }

        internal static List<vMovieUserRating> GetAll()
        {
            string functionName = "GetAll";
            if (ReadCache<List<vMovieUserRating>>(CacheAreaKey, functionName) != null) { return ReadCache<List<vMovieUserRating>>(CacheAreaKey, functionName); }

            Repository<vMovieUserRating> rep = new Repository<vMovieUserRating>(MArchiveDataContextProvider.Instance);
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
		}

		public static void Save ( MovieUserRatingDO dataObj, int userID ) {
			Repository<MOV_M_UserRating> rep = new Repository<MOV_M_UserRating> ( MArchiveDataContextProvider.Instance );

			MOV_M_UserRating rating = null;
			if ( dataObj.ID == 0 ) {
				rating = new MOV_M_UserRating ( );
				ObjectMapper.MapObjects ( dataObj, rating, AuditInfo.Fields );
				rep.InsertOnSubmit ( rating );
			} else {
				rating = rep.GetAll ( ).Single ( x => x.ID == dataObj.ID );
				ObjectMapper.MapObjects ( dataObj, rating, AuditInfo.Fields );
			}
            rep.DCP.CommitChanges(userID);

            InvalidateCache(CacheAreaKey);

			ObjectMapper.MapObjects ( dataObj, rating );
		}
	}
}