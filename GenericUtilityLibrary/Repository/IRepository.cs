using System.Linq;

namespace com.cagdaskorkut.utility.Repository
{
    interface IRepository
    {
        object GetById(int id);
        IQueryable GetAll();
        void InsertOnSubmit(object entity);
        void DeleteOnSubmit(object entity);
    }

    interface IRepository<T> where T : class
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        void InsertOnSubmit(T entity);
        void DeleteOnSubmit(T entity);
    }
}