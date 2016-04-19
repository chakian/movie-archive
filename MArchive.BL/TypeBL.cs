using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MArchive.DataContext;
using MArchive.Domain.Lookup;
using MArchiveLibrary.Repository;
using MArchiveLibrary.ObjectMapping;
using MArchiveLibrary.ExtendedDataContext;

namespace MArchive.BL {
    public class TypeBL : BLBase {
        private const string CacheAreaKey = "MArchive_Type";

		public static TypeDO Save ( TypeDO inputDO, int userID ) {
			Repository<INF_Type> rep = new Repository<INF_Type> ( MArchiveDataContextProvider.Instance );

			INF_Type objectToAdd = null;
			if ( inputDO.ID == 0 ) {
				objectToAdd = new INF_Type ( );
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

		internal static TypeDO GetDOByID( int ID ) {
			return GetAllDO( ).Single( q => q.ID == ID );
		}

		public static List<TypeDO> GetAllDO ( ) {
			return Mapper.Map<List<INF_Type>, List<TypeDO>>( GetAll( ).ToList( ) );
		}

        public static List<INF_Type> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<INF_Type>>(CacheAreaKey, functionName) != null) { return ReadCache<List<INF_Type>>(CacheAreaKey, functionName); }

			Repository<INF_Type> rep = new Repository<INF_Type> ( MArchiveDataContextProvider.Instance );
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
		}
	}
}