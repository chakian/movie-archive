using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using com.cagdaskorkut.utility.CacheUtil;
using com.cagdaskorkut.utility.ExtendedDataContext;
using com.cagdaskorkut.utility.ObjectMapping;
using com.cagdaskorkut.utility.Repository;
using MArchive.DataContext;
using MArchive.Domain.Lookup;

namespace MArchive.BL {
    public class DirectorBL : BLBase {
        private const string CacheAreaKey = "MArchive_Director";

		public static DirectorDO Save ( DirectorDO inputDO, int userID ) {
			Repository<INF_Director> rep = new Repository<INF_Director> ( MArchiveDataContextProvider.Instance );

			INF_Director objectToAdd = null;
			if ( inputDO.ID == 0 ) {
				objectToAdd = new INF_Director ( );
				ObjectMapper.MapObjects ( inputDO, objectToAdd, AuditInfo.Fields );
				rep.InsertOnSubmit ( objectToAdd );
			} else {
				objectToAdd = rep.GetAll ( ).Single ( x => x.ID == inputDO.ID );
				ObjectMapper.MapObjects ( inputDO, objectToAdd, AuditInfo.Fields );
			}
            rep.DCP.CommitChanges(userID);

            InvalidateCache(CacheAreaKey);

			ObjectMapper.MapObjects ( objectToAdd, inputDO );
			return inputDO;
		}

		internal static DirectorDO GetDOByID( int ID ) {
			return GetAllDO( ).Single( q => q.ID == ID );
		}

		public static List<DirectorDO> GetAllDO ( ) {
			return Mapper.Map<List<INF_Director>, List<DirectorDO>>( GetAll( ).ToList( ) );
		}

        public static List<INF_Director> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<INF_Director>>(CacheAreaKey, functionName) != null) { return ReadCache<List<INF_Director>>(CacheAreaKey, functionName); }

			Repository<INF_Director> rep = new Repository<INF_Director> ( MArchiveDataContextProvider.Instance );
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
		}
	}
}