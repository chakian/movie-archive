using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using com.cagdaskorkut.utility.CacheUtil;
using com.cagdaskorkut.utility.ExtendedDataContext;
using com.cagdaskorkut.utility.ObjectMapping;
using com.cagdaskorkut.utility.Repository;
using MArchive.DataContext;
using MArchive.Domain.Lookup;
using MArchive.Domain.Movie;

namespace MArchive.BL {
    public class MovieTypeBL : BLBase {
        private const string CacheAreaKey = "MArchive_MovieType";

		public static List<MovieTypeDO> GetAllDOByMovieID ( int movieID ) {
			return GetAllDO( ).Where( q => q.MovieID == movieID ).ToList( );
		}

		public static IQueryable<vMovieType> GetAllByMovieID( int movieID ) {
			return GetAll( ).Where( q => q.MovieID == movieID ).AsQueryable();
		}

        public static List<MovieTypeDO> GetAllDO() {
            string functionName = "GetAllDO";
            if (ReadCache<List<MovieTypeDO>>(CacheAreaKey, functionName) != null) { return ReadCache<List<MovieTypeDO>>(CacheAreaKey, functionName); }

			var allDO = Mapper.Map<List<vMovieType>, List<MovieTypeDO>>( GetAll( ).ToList( ) );

            WriteCache(CacheAreaKey, functionName, allDO);

            return allDO;
		}

        public static List<vMovieType> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<vMovieType>>(CacheAreaKey, functionName) != null) { return ReadCache<List<vMovieType>>(CacheAreaKey, functionName); }

            Repository<vMovieType> rep = new Repository<vMovieType>(MArchiveDataContextProvider.Instance);
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
		}

		public static MovieTypeDO Save ( MovieTypeDO inputDO, int userID ) {
			Repository<MOV_M_Type> rep = new Repository<MOV_M_Type> ( MArchiveDataContextProvider.Instance );

			MOV_M_Type objectToAdd = null;

			objectToAdd = new MOV_M_Type ( );
			ObjectMapper.MapObjects ( inputDO, objectToAdd, AuditInfo.Fields );
			rep.InsertOnSubmit ( objectToAdd );

            rep.DCP.CommitChanges(userID);

            InvalidateCache(CacheAreaKey);

			ObjectMapper.MapObjects ( objectToAdd, inputDO );
			return inputDO;
		}

		private static MovieTypeDO Save ( MOV_M_Type input, int userID ) {
			MovieTypeDO inputDO = new MovieTypeDO ( );
			ObjectMapper.MapObjects ( input, inputDO, AuditInfo.Fields );
			return Save ( inputDO, userID );
		}

		public static MovieTypeDO Save ( int movieID, string objectName, int userID ) {
			var objList = TypeBL.GetAllDO ( ).Where ( q => q.Name == objectName ).ToList ( );

			TypeDO obj;

			if ( objList != null && objList.Count > 0 ) {
				obj = objList[0];
			} else {
				obj = TypeBL.Save ( new TypeDO ( ) { Name = objectName }, userID );
			}

			MOV_M_Type objectToAdd = new MOV_M_Type ( );
			objectToAdd.MovieID = movieID;
			objectToAdd.TypeID = obj.ID;

			return Save ( objectToAdd, userID );
		}
	}
}