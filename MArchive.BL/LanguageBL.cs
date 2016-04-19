using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MArchive.DataContext;
using MArchive.Domain.Lookup;

namespace MArchive.BL {
    public class LanguageBL : BLBase {
        private const string CacheAreaKey = "MArchive_Language";

		public static LanguageDO Save ( LanguageDO inputDO, int userID ) {
			Repository<INF_Language> rep = new Repository<INF_Language> ( MArchiveDataContextProvider.Instance );

			INF_Language objectToAdd = null;
			if ( inputDO.ID == 0 ) {
				objectToAdd = new INF_Language ( );
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

		internal static LanguageDO GetDOByID( int ID ) {
			return GetAllDO( ).Single( q => q.ID == ID );
		}

		public static List<LanguageDO> GetAllDO ( ) {
			return Mapper.Map<List<INF_Language>, List<LanguageDO>>( GetAll( ).ToList( ) );
		}

        public static List<INF_Language> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<INF_Language>>(CacheAreaKey, functionName) != null) { return ReadCache<List<INF_Language>>(CacheAreaKey, functionName); }

			Repository<INF_Language> rep = new Repository<INF_Language> ( MArchiveDataContextProvider.Instance );
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
		}
	}
}