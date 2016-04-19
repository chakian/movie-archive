CREATE TABLE [dbo].[USR_FriendRequest] (
    [ID]                   INT      IDENTITY (1, 1) NOT NULL,
    [RequestCreatorUserID] INT      NOT NULL,
    [RequestSentToUserID]  INT      NOT NULL,
    [RequestStatus]        INT      CONSTRAINT [DF_USR_FriendRequest_RequestStatus] DEFAULT ((0)) NOT NULL,
    [InsertDate]           DATETIME NOT NULL,
    CONSTRAINT [PK_USR_FriendRequest] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_USR_FriendRequest_User] FOREIGN KEY ([RequestCreatorUserID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_USR_FriendRequest_User1] FOREIGN KEY ([RequestSentToUserID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_USR_FriendRequest_USR_FriendRequestStatus] FOREIGN KEY ([RequestStatus]) REFERENCES [dbo].[USR_FriendRequestStatus] ([ID])
);

