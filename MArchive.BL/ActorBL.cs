using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MArchive.DataContext;
using MArchive.Domain.Lookup;

namespace MArchive.BL {
	public class ActorBL : BLBase {
        private const string CacheAreaKey = "MArchive_Actor";

		public static ActorDO Save ( ActorDO inputDO, int userID ) {
			Repository<INF_Actor> rep = new Repository<INF_Actor> ( MArchiveDataContextProvider.Instance );

			INF_Actor objectToAdd = null;
			if ( inputDO.ID == 0 ) {
				objectToAdd = new INF_Actor ( );
				ObjectMapper.MapObjects ( inputDO, objectToAdd, AuditInfo.Fields );
				rep.InsertOnSubmit ( objectToAdd );
			} else {
				objectToAdd = rep.GetAll ( ).Single ( x => x.ID == inputDO.ID );
				ObjectMapper.MapObjects ( inputDO, objectToAdd, AuditInfo.Fields );
			}
			rep.DCP.CommitChanges ( userID );

            InvalidateCache(CacheAreaKey);

			ObjectMapper.MapObjects ( objectToAdd, inputDO );
			return inputDO;
		}

		internal static ActorDO GetDOByID( int ID ) {
			return GetAllDO( ).Single( q => q.ID == ID );
		}

		public static List<ActorDO> GetAllDO ( ) {
			return Mapper.Map<List<INF_Actor>, List<ActorDO>>( GetAll( ).ToList( ) );
		}

        public static List<INF_Actor> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<INF_Actor>>(CacheAreaKey, functionName) != null) { return ReadCache<List<INF_Actor>>(CacheAreaKey, functionName); }

			Repository<INF_Actor> rep = new Repository<INF_Actor> ( MArchiveDataContextProvider.Instance );
			var list = rep.GetAll ( ).ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
		}
    }
}