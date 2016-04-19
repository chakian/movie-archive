CREATE TABLE [dbo].[USR_Friend] (
    [ID]           INT      IDENTITY (1, 1) NOT NULL,
    [UserID]       INT      NOT NULL,
    [FriendUserID] INT      NOT NULL,
    [InsertDate]   DATETIME NOT NULL,
    CONSTRAINT [PK_USR_Friend] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_USR_Friend_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_USR_Friend_User1] FOREIGN KEY ([FriendUserID]) REFERENCES [dbo].[User] ([ID])
);



