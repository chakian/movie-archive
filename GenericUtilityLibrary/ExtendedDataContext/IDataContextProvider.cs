using System.Data.Linq;

namespace com.cagdaskorkut.utility.ExtendedDataContext
{
    public interface IDataContextProvider
    {
        DataContext GetDataContext();
        DataContext GetHistoryDataContext();
        void DestroyContext();
        CommitDBResult CommitChanges(int userId);
    }
}