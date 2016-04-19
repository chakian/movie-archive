using System.Linq;
using com.cagdaskorkut.utility.CacheUtil;
using com.cagdaskorkut.utility.ObjectMapping;
using com.cagdaskorkut.utility.Repository;
using MArchive.DataContext;
using MArchive.Domain.Movie;
using System.Collections.Generic;
using AutoMapper;

namespace MArchive.BL
{
    public class UserListBL : BLBase
    {
        private const string CacheAreaKey = "MArchive_MovieUserList";

        public static UserListDO GetListObject(int id)
        {
            return GetAllAsDO().Single(x => x.ID == id);
        }

        public static List<UserListDO> GetAllForUser(int userId)
        {
            return GetAllAsDO().Where(x => x.UserID == userId).ToList();
        }

        private static List<UserListDO> GetAllAsDO()
        {
            var list = GetAll();
            var movieList = Mapper.Map<List<USR_List>, List<UserListDO>>(list);
            return movieList;
        }

        internal static List<USR_List> GetAll()
        {
            string functionName = "GetAll";
            if (ReadCache<List<USR_List>>(CacheAreaKey, functionName) != null) { return ReadCache<List<USR_List>>(CacheAreaKey, functionName); }

            Repository<USR_List> rep = new Repository<USR_List>(MArchiveDataContextProvider.Instance);
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
        }

        public static UserListDO SaveUserList(UserListDO inputDO)
        {
            Repository<USR_List> rep = new Repository<USR_List>(MArchiveDataContextProvider.Instance);

            USR_List objectToAdd = null;
            if (inputDO.ID == 0)
            {
                objectToAdd = new USR_List();
                ObjectMapper.MapObjects(inputDO, objectToAdd);
                rep.InsertOnSubmit(objectToAdd);
            }
            else
            {
                objectToAdd = rep.GetAll().Single(x => x.ID == inputDO.ID);
                ObjectMapper.MapObjects(inputDO, objectToAdd);
            }
            rep.DCP.CommitChanges(inputDO.UserID);

            InvalidateCache(CacheAreaKey);

            ObjectMapper.MapObjects(objectToAdd, inputDO);
            return inputDO;
        }

        private static List<UserListMovieDO> GetListMovies(int listId)
        {
            return GetAllMoviesAsDO().Where(x => x.ListID == listId).ToList();
        }

        private static List<UserListMovieDO> GetAllMoviesAsDO()
        {
            var list = GetAllMovies();
            var movieList = Mapper.Map<List<USR_ListMovie>, List<UserListMovieDO>>(list);
            return movieList;
        }

        internal static List<USR_ListMovie> GetAllMovies()
        {
            string functionName = "GetAllMovies";
            if (ReadCache<List<USR_ListMovie>>(CacheAreaKey, functionName) != null) { return ReadCache<List<USR_ListMovie>>(CacheAreaKey, functionName); }

            Repository<USR_ListMovie> rep = new Repository<USR_ListMovie>(MArchiveDataContextProvider.Instance);
            var list = rep.GetAll().OrderBy(q => q.SortOrder).ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
        }

        public static void SaveToList(int ListID, int MovieID, int UserID)
        {
            var allListMovies = GetListMovies(ListID);
            if (allListMovies.Where(q => q.ListID == ListID && q.MovieID == MovieID).Count() == 0)
            {
                //calculate order
                int sortOrder = 0;
                if (allListMovies != null && allListMovies.Count > 0)
                {
                    sortOrder = allListMovies.OrderByDescending(q => q.SortOrder).First().SortOrder + 1;
                }
                SaveUserListMovie(ListID, MovieID, sortOrder, UserID);
            }
        }

        public static void SaveUserListMovie(int ListID, int MovieID, int SortOrder, int UserID)
        {
            Repository<USR_ListMovie> rep = new Repository<USR_ListMovie>(MArchiveDataContextProvider.Instance);

            USR_ListMovie objectToAdd = null;

            objectToAdd = new USR_ListMovie();
            objectToAdd.ListID = ListID;
            objectToAdd.MovieID = MovieID;
            objectToAdd.SortOrder = SortOrder;
            
            rep.InsertOnSubmit(objectToAdd);
            rep.DCP.CommitChanges(UserID);

            InvalidateCache(CacheAreaKey);
        }

        public static List<UserListDetailDO> GetAllViewByListID(int listID)
        {
            var list = GetAllView().Where(q => q.ListID == listID).ToList();
            var movieList = Mapper.Map<List<vUserListMoviesDetail>, List<UserListDetailDO>>(list);
            return movieList;
        }

        internal static List<UserListDetailDO> GetAllViewForUserThatIncludeCurrentMovie(int movieID, int userID)
        {
            var list = GetAllView().Where(q => q.UserID==userID && q.MovieID == movieID).ToList();
            var movieList = Mapper.Map<List<vUserListMoviesDetail>, List<UserListDetailDO>>(list);
            return movieList;
        }
        
        private static List<vUserListMoviesDetail> GetAllView()
        {
            string functionName = "GetAllView";
            if (ReadCache<List<vUserListMoviesDetail>>(CacheAreaKey, functionName) != null) { return ReadCache<List<vUserListMoviesDetail>>(CacheAreaKey, functionName); }

            Repository<vUserListMoviesDetail> rep = new Repository<vUserListMoviesDetail>(MArchiveDataContextProvider.Instance);
            var list = rep.GetAll().OrderBy(q => q.SortOrder).ToList();
            
            WriteCache(CacheAreaKey, functionName, list);

            return list;
        }
    }
}