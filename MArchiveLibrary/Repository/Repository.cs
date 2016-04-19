using MArchiveLibrary.ExtendedDataContext;
using MArchiveLibrary.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace MArchiveLibrary.Repository
{
    public class Repository<T> : IRepository<T>, IRepository where T : class
    {
        private static Dictionary<Type, bool> IsDisabledExistenceDictionary = new Dictionary<Type, bool>();

        private static bool HasIsDisabled(Type t)
        {
            if (IsDisabledExistenceDictionary.ContainsKey(t))
                return IsDisabledExistenceDictionary[t];

            var property = t.GetProperty("IsDisabled");
            bool retVal = (property != null);
            IsDisabledExistenceDictionary.Add(t, retVal);
            return retVal;
        }

        readonly DataContext dataContext;
        readonly DataContext historyDataContext;
        readonly IDataContextProvider dataContextProvider;

        public IDataContextProvider DCP
        {
            get
            {
                return dataContextProvider;
            }
        }

        private int UserID { get; set; }

        public DataLoadOptions LoadOptions
        {
            get { return dataContext.LoadOptions; }
            set { dataContext.LoadOptions = value; }
        }

        public Repository(IDataContextProvider _dataContextProvider, int _userId)
        {
            this.UserID = _userId;
            this.dataContextProvider = _dataContextProvider;
            dataContext = this.dataContextProvider.GetDataContext();
            historyDataContext = this.dataContextProvider.GetHistoryDataContext();
        }

        public Repository(IDataContextProvider _dataContextProvider)
        {
            this.dataContextProvider = _dataContextProvider;
            dataContext = this.dataContextProvider.GetDataContext();
            historyDataContext = dataContextProvider.GetHistoryDataContext();
        }



        private IQueryable<T> AddIsDisabledFilterIfExists(IQueryable<T> query)
        {
            var itemParameter = Expression.Parameter(typeof(T), "item");
            if (HasIsDisabled(typeof(T)))
            {
                var whereExpression2 = Expression.Lambda<Func<T, bool>>
                    (
                    Expression.Equal(Expression.Property(itemParameter, "IsDisabled"), Expression.Constant(false)),
                    new[] { itemParameter }
                    );
                query = query.Where(whereExpression2);

            }
            return query;
        }



        public virtual IQueryable<T> GetAll()
        {
            return dataContext.GetTable<T>();
        }

        public virtual IQueryable<T> GetAllActive()
        {
            var itemParameter = Expression.Parameter(typeof(T), "item");
            IQueryable<T> retVal = GetAll();
            retVal = AddIsDisabledFilterIfExists(retVal);
            return retVal;
        }

        private IQueryable<T> GetByIDAsQueryable(int id)
        {
            var itemParameter = Expression.Parameter(typeof(T), "item");
            var whereExpression = Expression.Lambda<Func<T, bool>>
                (
                Expression.Equal(Expression.Property(itemParameter, "ID"), Expression.Constant(id)),
                new[] { itemParameter }
                );
            return GetAll().Where(whereExpression);
        }

        public virtual T GetById(int id)
        {
            return GetByIDAsQueryable(id).SingleOrDefault();
        }

        public virtual T GetSingleById(int id)
        {
            return GetByIDAsQueryable(id).Single();
        }

        private IQueryable<T> GetByCodeAsQueryable(string code)
        {
            var itemParameter = Expression.Parameter(typeof(T), "item");
            var whereExpression = Expression.Lambda<Func<T, bool>>
                (
                Expression.Equal(Expression.Property(itemParameter, "Code"), Expression.Constant(code)),
                new[] { itemParameter }
                );
            return GetAll().Where(whereExpression);
        }

        public virtual T GetByCode(string code)
        {
            return GetByCodeAsQueryable(code).SingleOrDefault();
        }

        public virtual T GetSingleByCode(string code)
        {
            return GetByCodeAsQueryable(code).Single();
        }



        public virtual void UpdateByIdOnSubmit(T entity)
        {
            int ID = (int)(entity.GetType().GetProperty("ID").GetValue(entity, null));
            T existingEntity = GetSingleById(ID);
            ObjectMapper.CopyObject(entity, existingEntity, AuditInfo.Fields);
        }



        public virtual void InsertOnSubmit(T entity)
        {
            GetTable().InsertOnSubmit(entity);
        }

        public virtual void InsertAllOnSubmit(List<T> entityList)
        {
            GetTable().InsertAllOnSubmit(entityList);
        }



        public virtual void DeleteOnSubmit(T entity)
        {
            GetTable().DeleteOnSubmit(entity);
        }

        public virtual void DeleteByIdOnSubmit(int ID)
        {
            T existingEntity = GetSingleById(ID);
            DeleteOnSubmit(existingEntity);
        }

        public virtual ITable GetTable()
        {
            return dataContext.GetTable<T>();
        }

        public void DestroyRepository()
        {
            this.dataContextProvider.DestroyContext();
        }



        IQueryable IRepository.GetAll()
        {
            return GetAll();
        }

        void IRepository.InsertOnSubmit(object entity)
        {
            InsertOnSubmit((T)entity);
        }

        void IRepository.DeleteOnSubmit(object entity)
        {
            DeleteOnSubmit((T)entity);
        }

        object IRepository.GetById(int id)
        {
            return GetById(id);
        }
    }
}
