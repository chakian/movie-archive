using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MArchive.DataContext;
using MArchive.Domain.Lookup;
using MArchive.Domain.Movie;

namespace MArchive.BL {
    public class MovieWriterBL : BLBase {
        private const string CacheAreaKey = "MArchive_MovieWriter";

		public static List<MovieWriterDO> GetAllDOByMovieID ( int movieID ) {
			return GetAllDO( ).Where( q => q.MovieID == movieID ).ToList( );
		}

		public static IQueryable<vMovieWriter> GetAllByMovieID( int movieID ) {
			return GetAll( ).Where( q => q.MovieID == movieID ).AsQueryable();
		}

		public static List<MovieWriterDO> GetAllDO( ) {
            string functionName = "GetAllDO";
            if (ReadCache<List<MovieWriterDO>>(CacheAreaKey, functionName) != null) { return ReadCache<List<MovieWriterDO>>(CacheAreaKey, functionName); }

			var allDO = Mapper.Map<List<vMovieWriter>, List<MovieWriterDO>>( GetAll( ).ToList( ) );

            WriteCache(CacheAreaKey, functionName, allDO);

            return allDO;
		}

        public static List<vMovieWriter> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<vMovieWriter>>(CacheAreaKey, functionName) != null) { return ReadCache<List<vMovieWriter>>(CacheAreaKey, functionName); }

            Repository<vMovieWriter> rep = new Repository<vMovieWriter>(MArchiveDataContextProvider.Instance);
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
		}

		public static MovieWriterDO Save ( MovieWriterDO inputDO, int userID ) {
			Repository<MOV_M_Writer> rep = new Repository<MOV_M_Writer> ( MArchiveDataContextProvider.Instance );

			MOV_M_Writer objectToAdd = null;

			objectToAdd = new MOV_M_Writer ( );
			ObjectMapper.MapObjects ( inputDO, objectToAdd, AuditInfo.Fields );
			rep.InsertOnSubmit ( objectToAdd );

            rep.DCP.CommitChanges(userID);

            InvalidateCache(CacheAreaKey);

			ObjectMapper.MapObjects ( objectToAdd, inputDO );
			return inputDO;
		}

		private static MovieWriterDO Save ( MOV_M_Writer input, int userID ) {
			MovieWriterDO inputDO = new MovieWriterDO ( );
			ObjectMapper.MapObjects ( input, inputDO, AuditInfo.Fields );
			return Save ( inputDO, userID );
		}

		public static MovieWriterDO Save ( int movieID, string objectName, int userID ) {
			var objList = WriterBL.GetAllDO ( ).Where ( q => q.Name == objectName ).ToList ( );

			WriterDO obj;

			if ( objList != null && objList.Count > 0 ) {
				obj = objList[0];
			} else {
				obj = WriterBL.Save ( new WriterDO ( ) { Name = objectName }, userID );
			}

			MOV_M_Writer objectToAdd = new MOV_M_Writer ( );
			objectToAdd.MovieID = movieID;
			objectToAdd.WriterID = obj.ID;

			return Save ( objectToAdd, userID );
		}
	}
}