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
    public class WriterBL : BLBase {
        private const string CacheAreaKey = "MArchive_Writer";

		public static WriterDO Save ( WriterDO inputDO, int userID ) {
			Repository<INF_Writer> rep = new Repository<INF_Writer> ( MArchiveDataContextProvider.Instance );

			INF_Writer objectToAdd = null;
			if ( inputDO.ID == 0 ) {
				objectToAdd = new INF_Writer ( );
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

		internal static WriterDO GetDOByID( int ID ) {
			return GetAllDO( ).Single( q => q.ID == ID );
		}

		public static List<WriterDO> GetAllDO ( ) {
			return Mapper.Map<List<INF_Writer>, List<WriterDO>>( GetAll( ).ToList( ) );
		}

        public static List<INF_Writer> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<INF_Writer>>(CacheAreaKey, functionName) != null) { return ReadCache<List<INF_Writer>>(CacheAreaKey, functionName); }

			Repository<INF_Writer> rep = new Repository<INF_Writer> ( MArchiveDataContextProvider.Instance );
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
		}
	}
}