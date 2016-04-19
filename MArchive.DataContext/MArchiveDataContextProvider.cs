using System.Configuration;
using com.cagdaskorkut.utility.ExtendedDataContext;

namespace MArchive.DataContext
{
    public class MArchiveDataContextProvider : IDataContextProvider
    {
        public static MArchiveDataContextProvider Instance
        {
            get
            {
                return new MArchiveDataContextProvider();
            }
        }

        private MovieArchiveDBDataContext _dataContext;
        private MovieArchiveDBHistoryDataContext _historyDataContext;
        private static readonly string connectionStringName = "MArchiveConnectionString";

        public MovieArchiveDBDataContext GetMArchiveDataContext()
        {
            return (MovieArchiveDBDataContext)GetDataContext();
        }

        public System.Data.Linq.DataContext GetDataContext()
        {
            if (_dataContext == null)
                _dataContext = new MovieArchiveDBDataContext(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());

            return _dataContext;
        }

        public System.Data.Linq.DataContext GetHistoryDataContext()
        {
            if (_historyDataContext == null)
                _historyDataContext = new MovieArchiveDBHistoryDataContext(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());

            return _historyDataContext;
        }

        public void DestroyContext()
        {
            if (_dataContext != null)
            {
                _dataContext.Connection.Close();
                _dataContext.Dispose();
                _dataContext = null;
            }

            if (_historyDataContext != null)
            {
                _historyDataContext.Connection.Close();
                _historyDataContext.Dispose();
                _historyDataContext = null;
            }
        }

        public CommitDBResult CommitChanges(int UserID)
        {
            return DataContextHelper.CommitChanges(this.GetDataContext(), this.GetHistoryDataContext(), UserID);
        }
    }
}