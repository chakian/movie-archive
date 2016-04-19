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
    public class MovieLanguageBL : BLBase {
        private const string CacheAreaKey = "MArchive_MovieLanguage";

		public static List<MovieLanguageDO> GetAllDOByMovieID ( int movieID ) {
			return GetAllDO( ).Where( q => q.MovieID == movieID ).ToList( );
		}

		public static IQueryable<vMovieLanguage> GetAllByMovieID( int movieID ) {
			return GetAll( ).Where( q => q.MovieID == movieID ).AsQueryable();
		}

        public static List<MovieLanguageDO> GetAllDO() {
            string functionName = "GetAllDO";
            if (ReadCache<List<MovieLanguageDO>>(CacheAreaKey, functionName) != null) { return ReadCache<List<MovieLanguageDO>>(CacheAreaKey, functionName); }

			var allDO = Mapper.Map<List<vMovieLanguage>, List<MovieLanguageDO>>( GetAll( ).ToList( ) );

            WriteCache(CacheAreaKey, functionName, allDO);

            return allDO;
		}

        public static List<vMovieLanguage> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<vMovieLanguage>>(CacheAreaKey, functionName) != null) { return ReadCache<List<vMovieLanguage>>(CacheAreaKey, functionName); }

            Repository<vMovieLanguage> rep = new Repository<vMovieLanguage>(MArchiveDataContextProvider.Instance);
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
		}

		public static MovieLanguageDO Save ( MovieLanguageDO inputDO, int userID ) {
			Repository<MOV_M_Language> rep = new Repository<MOV_M_Language> ( MArchiveDataContextProvider.Instance );

			MOV_M_Language objectToAdd = null;

			objectToAdd = new MOV_M_Language ( );
			ObjectMapper.MapObjects ( inputDO, objectToAdd, AuditInfo.Fields );
			rep.InsertOnSubmit ( objectToAdd );

            rep.DCP.CommitChanges(userID);

            InvalidateCache(CacheAreaKey);

			ObjectMapper.MapObjects ( objectToAdd, inputDO );
			return inputDO;
		}

		private static MovieLanguageDO Save ( MOV_M_Language input, int userID ) {
			MovieLanguageDO inputDO = new MovieLanguageDO ( );
			ObjectMapper.MapObjects ( input, inputDO, AuditInfo.Fields );
			return Save ( inputDO, userID );
		}

		public static MovieLanguageDO Save ( int movieID, string objectName, int userID ) {
			var objList = LanguageBL.GetAllDO ( ).Where ( q => q.Name == objectName ).ToList ( );

			LanguageDO obj;

			if ( objList != null && objList.Count > 0 ) {
				obj = objList[0];
			} else {
				obj = LanguageBL.Save ( new LanguageDO ( ) { Name = objectName }, userID );
			}

			MOV_M_Language objectToAdd = new MOV_M_Language ( );
			objectToAdd.MovieID = movieID;
			objectToAdd.LanguageID = obj.ID;

			return Save ( objectToAdd, userID );
		}
	}
}