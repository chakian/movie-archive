using MArchiveLibrary.Reflection;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;

namespace MArchiveLibrary.ExtendedDataContext
{
    public class DataContextHelper
    {
        private static string[] AllowedTypes = { "System.ValueType", "System.Object" };

        public static CommitDBResult CommitChanges(DataContext _dataContext, DataContext _historyDataContext, int _userID)
        {
            CommitDBResult commitDBResult = CommitDBResult.Success;
            CheckForInvalidVersions(_dataContext);
            SetAuditInfo(_dataContext, _userID);

            WriteUpdatesAndDeletes(_dataContext, _historyDataContext, _userID);

            _dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);

            return commitDBResult;
        }

        private static void CheckForInvalidVersions(DataContext _dataContext)
        {
            var changes = _dataContext.GetChangeSet();
            foreach (object item in changes.Updates)
            {
                ITable table = _dataContext.GetTable(item.GetType());
                object originalItem = table.GetOriginalEntityState(item);

                System.Data.Linq.Binary originalVerCol = (System.Data.Linq.Binary)originalItem.GetPropertyValue("VerCol");
                System.Data.Linq.Binary currentVerCol = (System.Data.Linq.Binary)item.GetPropertyValue("VerCol");
                if (!System.Data.Linq.Binary.Equals(currentVerCol, originalVerCol))
                {
                    throw new ChangeConflictException(string.Format("{0} object instance with ID ({1}) being modified with rowversion({2}) has different rowversion({3}) in database.", item.GetType().Name, item.GetPropertyValue("ID"), currentVerCol, originalVerCol));
                }
            }
        }

        public static CommitDBResult CommitChanges(IDataContextProvider dataContextProvider, int userID)
        {
            return CommitChanges(dataContextProvider.GetDataContext(), dataContextProvider.GetHistoryDataContext(), userID);
        }

        /// <summary>
        /// Sets the InsertUserId, InsertDate, UpdateuserId, UpdateDate
        /// </summary>
        private static void SetAuditInfo(DataContext _dataContext, int _userID)
        {
            AuditInfo auditInfo = new AuditInfo(_dataContext, _userID);
            auditInfo.setUpdateColumns();
        }

        private static void WriteUpdatesAndDeletes(DataContext _dataContext, DataContext _historyDataContext, int _userID)
        {
            ProcessUpdateItems(_dataContext, _historyDataContext, _userID);
            ProcessDeleteItems(_dataContext, _historyDataContext, _userID);
            _historyDataContext.SubmitChanges();
        }

        private static void setProperty(object tableName, string propertyName, int propertyValue)
        {
            PropertyInfo insertBy = tableName.GetType().GetProperty(propertyName);

            if (insertBy != null)
            {
                tableName.GetType().GetProperty(propertyName).SetValue(tableName, propertyValue, null);
            }
        }

        private static void setProperty(object tableName, string propertyName, DateTime propertyValue)
        {
            PropertyInfo prop = tableName.GetType().GetProperty(propertyName);

            if (prop != null)
            {
                tableName.GetType().GetProperty(propertyName).SetValue(tableName, propertyValue, null);
            }
        }

        private static void SetColumns(IList<object> items, CommitActionType actionType, int _userID)
        {
            foreach (object item in items)
            {
                setProperty(item, AuditInfo.ActionUserID, _userID);
                setProperty(item, AuditInfo.ActionTime, DateTime.Now);
            }
        }

        private static void ProcessDeleteItems(DataContext _dataContext, DataContext _historyDataContext, int _userID)
        {
            ChangeSet CurrentChangeSet = _dataContext.GetChangeSet();
            foreach (var item in CurrentChangeSet.Deletes)
            {
                CopyTableColumns(item, CommitActionType.Delete, _historyDataContext, _userID);
            }
        }

        private static void ProcessUpdateItems(DataContext _dataContext, DataContext _historyDataContext, int _userID)
        {
            ChangeSet CurrentChangeSet = _dataContext.GetChangeSet();

            foreach (object item in CurrentChangeSet.Updates)
            {
                ITable table = _dataContext.GetTable(item.GetType());
                object originalItem = table.GetOriginalEntityState(item);

                CommitActionType actionType = CommitActionType.Update;

                CopyTableColumns(originalItem, actionType, _historyDataContext, _userID);
            }
        }

        private static void CopyTableColumns(object item, CommitActionType actionType, DataContext _targetDataContext, int _userID)
        {
            string typeName = item.GetType().AssemblyQualifiedName.Replace(item.GetType().FullName, item.GetType().FullName + "_History");

            Type historyType = Type.GetType(typeName);

            if (historyType != null)
            {
                ObjectHandle instance = Activator.CreateInstance(historyType.Assembly.FullName, item.GetType().FullName + "_History");

                string tableName = item.GetType().Name;
                List<PropertyInfo> columns = new List<PropertyInfo>();

                foreach (PropertyInfo column in item.GetType().GetProperties())
                {
                    if (AllowedTypes.Contains(column.PropertyType.BaseType.FullName))
                        columns.Add(column);
                }

                foreach (var column in instance.Unwrap().GetType().GetProperties())
                {
                    if (columns.FirstOrDefault(q => q.Name == column.Name) != null)
                    {
                        string colName = columns.FirstOrDefault(q => q.Name == column.Name).Name;
                        object val = item.GetType().GetProperty(colName).GetValue(item, null);
                        column.SetValue(instance.Unwrap(), val, null);
                    }
                }

                setProperty(instance.Unwrap(), AuditInfo.ActionTime, DateTime.UtcNow);
                setProperty(instance.Unwrap(), AuditInfo.ActionUserID, _userID);
                setProperty(instance.Unwrap(), AuditInfo.Action, Convert.ToInt32(actionType));

                _targetDataContext.GetTable(instance.Unwrap().GetType()).InsertOnSubmit(instance.Unwrap());
            }
        }
    }
}
