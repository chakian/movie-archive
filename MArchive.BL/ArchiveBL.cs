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
    public class ArchiveBL : BLBase {
        private const string CacheAreaKey = "MArchive_Archive";

        public static List<ArchiveDO> GetAllDOByUserID(int userID) {
            return GetAllDO().Where(q => q.UserID == userID).ToList();
        }

        public static ArchiveDO Save(ArchiveDO inputDO, int userID) {
            Repository<USR_Archive> rep = new Repository<USR_Archive>(MArchiveDataContextProvider.Instance);

            USR_Archive objectToAdd = null;
            if (inputDO.ID == 0) {
                objectToAdd = new USR_Archive();
                ObjectMapper.MapObjects(inputDO, objectToAdd, AuditInfo.Fields);
                rep.InsertOnSubmit(objectToAdd);
            } else {
                objectToAdd = rep.GetAll().Single(x => x.ID == inputDO.ID);
                ObjectMapper.MapObjects(inputDO, objectToAdd, AuditInfo.Fields);
            }
            rep.DCP.CommitChanges(userID);

            InvalidateCache(CacheAreaKey);

            ObjectMapper.MapObjects(objectToAdd, inputDO);
            return inputDO;
        }

        internal static ArchiveDO GetDOByID(int ID) {
            return GetAllDO().Single(q => q.ID == ID);
        }

        internal static List<ArchiveDO> GetAllDO() {
            return Mapper.Map<List<USR_Archive>, List<ArchiveDO>>(GetAll());
        }

        internal static List<USR_Archive> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<USR_Archive>>(CacheAreaKey, functionName) != null) { return ReadCache<List<USR_Archive>>(CacheAreaKey, functionName); }

            Repository<USR_Archive> rep = new Repository<USR_Archive>(MArchiveDataContextProvider.Instance);
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
        }
    }
}