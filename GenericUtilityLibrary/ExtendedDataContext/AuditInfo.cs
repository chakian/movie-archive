using System;
using System.Data.Linq;
using System.Reflection;

namespace com.cagdaskorkut.utility.ExtendedDataContext
{
    public class AuditInfo
    {
        #region Fields
        public static string[] Fields
        {
            get
            {
                string[] fieldArr = new string[6]{
                    InsertUserID,
                    InsertDate,
                    UpdateUserID,
                    UpdateDate,
                    ActionUserID,
                    ActionTime
                };

                return fieldArr;
            }
        }
        public static string InsertUserID = "InsertUserID";
        public static string InsertDate = "InsertDate";
        public static string UpdateUserID = "UpdateUserID";
        public static string UpdateDate = "UpdateDate";
        public static string ActionTime = "ActionTime";
        public static string ActionUserID = "ActionUserID";
        public static string Action = "Action";
        public DataContext dataContext;
        public int userId;
        #endregion

        public AuditInfo(DataContext dataContext, int userId)
        {
            this.dataContext = dataContext;
            this.userId = userId;
        }

        private void setIntProperty(object tableName, string propertyName, int propertyValue)
        {
            PropertyInfo propertyInfo = tableName.GetType().GetProperty(propertyName);

            if (propertyInfo != null)
            {
                tableName.GetType().GetProperty(propertyName).SetValue(tableName, propertyValue, null);
            }
        }

        private void setDateTimeProperty(object tableName, string propertyName, DateTime propertyValue)
        {
            PropertyInfo propertyInfo = tableName.GetType().GetProperty(propertyName);

            if (propertyInfo != null)
            {
                tableName.GetType().GetProperty(propertyName).SetValue(tableName, propertyValue, null);
            }
        }

        public void setUpdateColumns()
        {
            var changes = dataContext.GetChangeSet();

            foreach (object item in changes.Updates)
            {
                setIntProperty(item, UpdateUserID, userId);
                setDateTimeProperty(item, UpdateDate, DateTime.UtcNow);
            }
            foreach (object item in changes.Inserts)
            {
                setIntProperty(item, UpdateUserID, userId);
                setDateTimeProperty(item, UpdateDate, DateTime.UtcNow);
                setIntProperty(item, InsertUserID, userId);
                setDateTimeProperty(item, InsertDate, DateTime.UtcNow);
            }
        }
    }
}