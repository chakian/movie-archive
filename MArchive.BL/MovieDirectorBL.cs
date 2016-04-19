using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MArchive.DataContext;
using MArchive.Domain.Lookup;
using MArchive.Domain.Movie;
using MArchiveLibrary.Repository;
using MArchiveLibrary.ObjectMapping;
using MArchiveLibrary.ExtendedDataContext;

namespace MArchive.BL {
    public class MovieDirectorBL : BLBase {
        private const string CacheAreaKey = "MArchive_MovieDirector";

		public static List<MovieDirectorDO> GetAllDOByMovieID ( int movieID ) {
			return GetAllDO( ).Where( q => q.MovieID == movieID ).ToList( );
		}

		public static IQueryable<vMovieDirector> GetAllByMovieID( int movieID ) {
			return GetAll( ).Where( q => q.MovieID == movieID ).AsQueryable();
		}

        public static List<MovieDirectorDO> GetAllDO() {
            string functionName = "GetAllDO";
            if (ReadCache<List<MovieDirectorDO>>(CacheAreaKey, functionName) != null) { return ReadCache<List<MovieDirectorDO>>(CacheAreaKey, functionName); }

			var allDO = Mapper.Map<List<vMovieDirector>, List<MovieDirectorDO>>( GetAll( ).ToList( ) );

            WriteCache(CacheAreaKey, functionName, allDO);

            return allDO;
		}

        public static List<vMovieDirector> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<vMovieDirector>>(CacheAreaKey, functionName) != null) { return ReadCache<List<vMovieDirector>>(CacheAreaKey, functionName); }

            Repository<vMovieDirector> rep = new Repository<vMovieDirector>(MArchiveDataContextProvider.Instance);
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
		}

		public static MovieDirectorDO Save ( MovieDirectorDO inputDO, int userID ) {
			Repository<MOV_M_Director> rep = new Repository<MOV_M_Director> ( MArchiveDataContextProvider.Instance );

			MOV_M_Director objectToAdd = null;

			objectToAdd = new MOV_M_Director ( );
			ObjectMapper.MapObjects ( inputDO, objectToAdd, AuditInfo.Fields );
			rep.InsertOnSubmit ( objectToAdd );

            rep.DCP.CommitChanges(userID);

            InvalidateCache(CacheAreaKey);

			ObjectMapper.MapObjects ( objectToAdd, inputDO );
			return inputDO;
		}

		private static MovieDirectorDO Save ( MOV_M_Director input, int userID ) {
			MovieDirectorDO inputDO = new MovieDirectorDO ( );
			ObjectMapper.MapObjects ( input, inputDO, AuditInfo.Fields );
			return Save ( inputDO, userID );
		}

		public static MovieDirectorDO Save ( int movieID, string objectName, int userID ) {
			var objList = DirectorBL.GetAllDO ( ).Where ( q => q.Name == objectName ).ToList ( );

			DirectorDO obj;

			if ( objList != null && objList.Count > 0 ) {
				obj = objList[0];
			} else {
				obj = DirectorBL.Save ( new DirectorDO ( ) { Name = objectName }, userID );
			}

			MOV_M_Director objectToAdd = new MOV_M_Director ( );
			objectToAdd.MovieID = movieID;
			objectToAdd.DirectorID = obj.ID;

			return Save ( objectToAdd, userID );
		}
	}
}