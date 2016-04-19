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
    public class MovieActorBL : BLBase {
        private const string CacheAreaKey = "MArchive_MovieActor";

		public static List<MovieActorDO> GetAllDOByMovieID( int movieID ) {
			return GetAllDO( ).Where( q => q.MovieID == movieID ).ToList( );
		}

		public static IQueryable<vMovieActor> GetAllByMovieID ( int movieID ) {
			return GetAll( ).Where( q => q.MovieID == movieID ).AsQueryable();
		}

        public static List<MovieActorDO> GetAllDO() {
            string functionName = "GetAllDO";
            if (ReadCache<List<MovieActorDO>>(CacheAreaKey, functionName) != null) { return ReadCache<List<MovieActorDO>>(CacheAreaKey, functionName); }

			var allDO = Mapper.Map<List<vMovieActor>, List<MovieActorDO>>( GetAll( ).ToList( ) );
            
            WriteCache(CacheAreaKey, functionName, allDO);

            return allDO;
		}

        public static List<vMovieActor> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<vMovieActor>>(CacheAreaKey, functionName) != null) { return ReadCache<List<vMovieActor>>(CacheAreaKey, functionName); }

			Repository<vMovieActor> rep = new Repository<vMovieActor>( MArchiveDataContextProvider.Instance );
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
		}

		public static MovieActorDO Save( MovieActorDO inputDO, int userID ) {
			Repository<MOV_M_Actor> rep = new Repository<MOV_M_Actor>( MArchiveDataContextProvider.Instance );

			MOV_M_Actor objectToAdd = null;

			objectToAdd = new MOV_M_Actor( );
			ObjectMapper.MapObjects( inputDO, objectToAdd, AuditInfo.Fields );
			rep.InsertOnSubmit( objectToAdd );

            rep.DCP.CommitChanges(userID);

            InvalidateCache(CacheAreaKey);

			ObjectMapper.MapObjects( objectToAdd, inputDO );
			return inputDO;
		}

		private static MovieActorDO Save( MOV_M_Actor input, int userID ) {
			MovieActorDO inputDO = new MovieActorDO( );
			ObjectMapper.MapObjects( input, inputDO, AuditInfo.Fields );
			return Save( inputDO, userID );
		}

		public static MovieActorDO Save( int movieID, string objectName, int userID ) {
			var objList = ActorBL.GetAllDO( ).Where( q => q.Name == objectName ).ToList( );

			ActorDO obj;

			if( objList != null && objList.Count > 0 ) {
				obj = objList[0];
			} else {
				obj = ActorBL.Save( new ActorDO( ) { Name = objectName }, userID );
			}

			MOV_M_Actor objectToAdd = new MOV_M_Actor( );
			objectToAdd.MovieID = movieID;
			objectToAdd.ActorID = obj.ID;

			return Save( objectToAdd, userID );
		}
	}
}