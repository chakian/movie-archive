using System.Data.Linq;

namespace MArchiveLibrary.ExtendedDataContext
{
    public interface IDataContextProvider
    {
        DataContext GetDataContext();
        DataContext GetHistoryDataContext();
        void DestroyContext();
        CommitDBResult CommitChanges(int userId);
    }
}
