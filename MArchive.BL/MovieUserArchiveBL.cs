using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MArchive.DataContext;
using MArchive.Domain.Movie;

namespace MArchive.BL {
    public class MovieUserArchiveBL : BLBase {
        private const string CacheAreaKey = "MArchive_MovieArchive";

        public static List<MovieUserArchiveDO> GetAllDOByMovieIDAndUserID(int movieID, int userID) {
			return GetAllDO( ).Where( q => q.MovieID == movieID && q.UserID == userID ).ToList( );
		}
        

        //private static MovieArchiveDO Save(MovieArchiveDO inputDO, int userID) {
        //    Repository<MOV_M_Archive> rep = new Repository<MOV_M_Archive> ( MArchiveDataContextProvider.Instance );

        //    MOV_M_Archive objectToAdd = null;
        //    if ( inputDO.ID == 0 ) {
        //        objectToAdd = new MOV_M_Archive ( );
        //        ObjectMapper.MapObjects ( inputDO, objectToAdd, AuditInfo.Fields );
        //        rep.InsertOnSubmit ( objectToAdd );
        //    } else {
        //        objectToAdd = rep.GetAll ( ).Single ( x => x.ID == inputDO.ID );
        //        ObjectMapper.MapObjects ( inputDO, objectToAdd, AuditInfo.Fields );
        //    }
        //    rep.DCP.CommitChanges(userID);

        //    InvalidateCache(CacheAreaKey);

        //    ObjectMapper.MapObjects ( objectToAdd, inputDO );
        //    return inputDO;
        //}

        //private static MovieArchiveDO Save ( MOV_M_Archive input, int userID ) {
        //    MovieArchiveDO inputDO = new MovieArchiveDO ( );
        //    ObjectMapper.MapObjects ( input, inputDO, AuditInfo.Fields );
        //    return Save ( inputDO, userID );
        //}

        //private static MovieArchiveDO Save ( int movieID, string objectName, int userID ) {
        //    var objList = ArchiveBL.GetAllDO ( ).Where ( q => q.Name == objectName && q.UserID == userID ).ToList ( );

        //    ArchiveDO obj;

        //    if ( objList != null && objList.Count > 0 ) {
        //        obj = objList[0];
        //    } else {
        //        obj = ArchiveBL.Save(new ArchiveDO() { Name = objectName, UserID = userID }, userID);
        //    }

        //    MOV_M_Archive objectToAdd = new MOV_M_Archive ( );
        //    objectToAdd.MovieID = movieID;
        //    objectToAdd.ArchiveID = obj.ID;

        //    return Save ( objectToAdd, userID );
        //}

        internal static List<MovieUserArchiveDO> GetAllDO() {
            string functionName = "GetAllDO";
            if (ReadCache<List<MovieUserArchiveDO>>(CacheAreaKey, functionName) != null) { return ReadCache<List<MovieUserArchiveDO>>(CacheAreaKey, functionName); }

            var allDO = Mapper.Map<List<vMovieArchive>, List<MovieUserArchiveDO>>(GetAll());

            WriteCache(CacheAreaKey, functionName, allDO);

            return allDO;
        }

        internal static List<vMovieArchive> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<vMovieArchive>>(CacheAreaKey, functionName) != null) { return ReadCache<List<vMovieArchive>>(CacheAreaKey, functionName); }

            Repository<vMovieArchive> rep = new Repository<vMovieArchive>(MArchiveDataContextProvider.Instance);
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
        }
	}
}