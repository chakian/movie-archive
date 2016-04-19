using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MArchive.DataContext;
using MArchive.Domain.User;
using MArchiveLibrary.Helpers;
using MArchiveLibrary.Exceptions;
using MArchiveLibrary.Repository;

namespace MArchive.BL {
	public class UserBL : BLBase {
        internal const string CacheAreaKey = "MArchive_User";

		internal static User GetUserByUsername( string username ) {
            return GetAllUsers().Where(u => u.Username.ToLowerInvariant() == username.ToLowerInvariant()).SingleOrDefault();
		}
        public static UserDO GetUserDOByUsername(string username) {
            return Mapper.Map<User, UserDO>(GetUserByUsername(username));
        }

		public static bool IsUserAuthenticationCorrect( UserDO user, string username, string password ) {
			// check user data validity
            if (user == null || user.Password == EncryptionHelper.Encrypt(password) == false) {
                throw new BusinessException("Incorrect username or password.");
			}

			return true;
		}

        public static List<UserDO> SearchByExactUsername(string username) {
            List<User> userList = GetAllUsers().Where(u => u.Username == username).ToList();
            return Mapper.Map<List<User>, List<UserDO>>(userList);
        }
        public static List<UserDO> SearchByLikelyUsername(string username) {
            List<User> userList = GetAllUsers().Where(u => u.Username.Contains(username)).ToList();
            return Mapper.Map<List<User>, List<UserDO>>(userList);
        }

        public static List<UserDO> GetAllUsersDO()
        {
            return Mapper.Map<List<User>, List<UserDO>>(GetAllUsers());
        }

        internal static List<User> GetAllUsers( ) {
            string functionName = "GetAllUsers";
            if (ReadCache<List<User>>(CacheAreaKey, functionName) != null) { return ReadCache<List<User>>(CacheAreaKey, functionName); }

			Repository<User> rep = new Repository<User>( MArchiveDataContextProvider.Instance );
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

			return list;
		}
	}
}