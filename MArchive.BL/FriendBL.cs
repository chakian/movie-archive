using MArchive.DataContext;
using MArchive.Domain.Message;
using MArchive.Domain.User;
using MArchiveLibrary.ObjectMapping;
using System.Collections.Generic;
using System.Linq;
using MArchiveLibrary.Repository;
using MArchiveLibrary.ExtendedDataContext;

namespace MArchive.BL
{
    public class FriendBL : BLBase
    {
        internal const string CacheAreaKey = "MArchive_Friend";

        private static List<int> GetThisUsersFriendsIds(int thisUser)
        {
            List<int> friendIdsList = new List<int>();
            friendIdsList.AddRange(GetAllUserFriends().Where(q => q.UserID == thisUser).Select(q => q.FriendUserID));
            friendIdsList.AddRange(GetAllUserFriends().Where(q => q.FriendUserID == thisUser).Select(q => q.UserID));
            return friendIdsList;
        }

        private static List<USR_FriendRequest> GetActiveFriendRequestsForUser(int thisUser)
        {
            List<USR_FriendRequest> friendRequestsList = new List<USR_FriendRequest>();
            friendRequestsList.AddRange(GetAllFriendRequests().Where(q=>q.RequestStatus == (int)FriendRequestStatusType.Requested)
                .Where(q => q.RequestCreatorUserID == thisUser || q.RequestSentToUserID == thisUser));
            return friendRequestsList;
        }

        public static FriendStatus GetFriendshipStatus(int thisUser, int thatUser)
        {
            FriendStatus status = new FriendStatus()
            {
                ThisUser = thisUser,
                ThatUser = thatUser
            };

            if (GetThisUsersFriendsIds(thisUser).Contains(thatUser) == true)
            {
                status.RequestStatusType = FriendRequestStatusType.Friends;
            }
            else
            {
                var allRequests = GetActiveFriendRequestsForUser(thisUser).OrderByDescending(q => q.InsertDate);

                if(allRequests != null && allRequests.Count() > 0)
                {
                    if (allRequests.FirstOrDefault(q => q.RequestSentToUserID == thisUser) != null)
                    {
                        status.RequestStatusType = FriendRequestStatusType.TheyRequested;
                    }
                    else
                    {
                        status.RequestStatusType = FriendRequestStatusType.Requested;
                    }
                }
            }

            return status;
        }

        private static USR_FriendRequest GetLatestFriendRequestMadeByThatUserToThisUser(int thatUser, int thisUser)
        {
            var request = GetAllFriendRequests()
                .Where(q => q.RequestStatus==(int)FriendRequestStatusType.Requested && q.RequestCreatorUserID==thatUser && q.RequestSentToUserID==thisUser)
                .OrderByDescending(q => q.InsertDate).FirstOrDefault();
            return request;
        }

        public static void SendFriendRequest(UserFriendRequestDO requestDO)
        {
            SaveFriendRequest(requestDO, requestDO.RequestCreatorUserID);
        }
        public static GenericResponseMessage AcceptFriendRequest(int thisUser, int thatUser)
        {
            GenericResponseMessage response = new GenericResponseMessage();

            var request = GetLatestFriendRequestMadeByThatUserToThisUser(thatUser, thisUser);

            if (GetThisUsersFriendsIds(thisUser).Contains(thatUser))
            {
                response.AddError(MessageCodes.TheseUsersAreAlreadyFriends);
            }
            else
            {
                if (request == null)
                {
                    response.AddError(MessageCodes.ThereIsNoRequestToAccept);
                }
                else if(request.RequestStatus != (int)FriendRequestStatusType.Requested)
                {
                    response.AddError(MessageCodes.ThereIsNoRequestToAccept);
                }
            }

            if (response.IsSuccessful)
            {
                UserFriendRequestDO ufDataObj = new UserFriendRequestDO();
                request.RequestStatus = (int)FriendRequestStatusType.Friends;
                ObjectMapper.MapObjects(request, ufDataObj);
                SaveFriendRequest(ufDataObj, thisUser);

                UserFriendDO uDataObj = new UserFriendDO();
                uDataObj.UserID = thisUser;
                uDataObj.FriendUserID = thatUser;
                SaveFriend(uDataObj, thisUser);
            }

            return response;
        }
        public static GenericResponseMessage RejectFriendRequest(int thisUser, int thatUser)
        {
            GenericResponseMessage response = new GenericResponseMessage();

            var friendRequest = GetLatestFriendRequestMadeByThatUserToThisUser(thatUser, thisUser);

            if(friendRequest == null || friendRequest.RequestStatus != (int)FriendRequestStatusType.Requested)
            {
                response.AddError(MessageCodes.ThereIsNoRequestToReject);
            }

            if (response.IsSuccessful)
            {
                UserFriendRequestDO ufDataObj = new UserFriendRequestDO();
                friendRequest.RequestStatus = (int)FriendRequestStatusType.Rejected;
                ObjectMapper.MapObjects<USR_FriendRequest, UserFriendRequestDO>(friendRequest, ufDataObj);
                SaveFriendRequest(ufDataObj, thisUser);
                
                if (GetThisUsersFriendsIds(thisUser).Contains(thatUser))
                {
                    DeleteFriend(thatUser, thisUser);
                }
            }

            return response;
        }

        #region cache changing functions
        private static List<USR_Friend> GetAllUserFriends()
        {
            string functionName = "GetAllUserFriends";
            if (ReadCache<List<USR_Friend>>(CacheAreaKey, functionName) != null) { return ReadCache<List<USR_Friend>>(CacheAreaKey, functionName); }

            Repository<USR_Friend> rep = new Repository<USR_Friend>(MArchiveDataContextProvider.Instance);
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
        }

        private static List<USR_FriendRequest> GetAllFriendRequests()
        {
            string functionName = "GetAllFriendRequests";
            if (ReadCache<List<USR_FriendRequest>>(CacheAreaKey, functionName) != null) { return ReadCache<List<USR_FriendRequest>>(CacheAreaKey, functionName); }

            Repository<USR_FriendRequest> rep = new Repository<USR_FriendRequest>(MArchiveDataContextProvider.Instance);
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
        }

        private static void SaveFriend(UserFriendDO dataObj, int userID)
        {
            Repository<USR_Friend> rep = new Repository<USR_Friend>(MArchiveDataContextProvider.Instance);

            USR_Friend objectToAdd = null;
            if (dataObj.ID == 0)
            {
                objectToAdd = new USR_Friend();
                ObjectMapper.MapObjects(dataObj, objectToAdd, AuditInfo.Fields);
                rep.InsertOnSubmit(objectToAdd);
            }
            else
            {
                objectToAdd = rep.GetAll().Single(x => x.ID == dataObj.ID);
                ObjectMapper.MapObjects(dataObj, objectToAdd, AuditInfo.Fields);
            }
            rep.DCP.CommitChanges(userID);

            InvalidateCache(CacheAreaKey);

            ObjectMapper.MapObjects(objectToAdd, dataObj);
        }
        private static void DeleteFriend(int ID, int userID)
        {
            Repository<USR_Friend> rep = new Repository<USR_Friend>(MArchiveDataContextProvider.Instance);
            rep.DeleteByIdOnSubmit(ID);
            rep.DCP.CommitChanges(userID);
            
            InvalidateCache(CacheAreaKey);
        }
        private static void SaveFriendRequest(UserFriendRequestDO dataObj, int userID)
        {
            Repository<USR_FriendRequest> rep = new Repository<USR_FriendRequest>(MArchiveDataContextProvider.Instance);

            USR_FriendRequest objectToAdd = null;
            if (dataObj.ID == 0)
            {
                objectToAdd = new USR_FriendRequest();
                ObjectMapper.MapObjects(dataObj, objectToAdd, AuditInfo.Fields);
                rep.InsertOnSubmit(objectToAdd);
            }
            else
            {
                objectToAdd = rep.GetAll().Single(x => x.ID == dataObj.ID);
                ObjectMapper.MapObjects(dataObj, objectToAdd, AuditInfo.Fields);
            }
            rep.DCP.CommitChanges(userID);

            InvalidateCache(CacheAreaKey);

            ObjectMapper.MapObjects(objectToAdd, dataObj);
        }
        #endregion cache changing functions
    }
}