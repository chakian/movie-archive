CREATE TABLE [dbo].[USR_FriendRequestStatus] (
    [ObligatoryID] INT           IDENTITY (1, 1) NOT NULL,
    [ID]           INT           NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_USR_FriendRequestStatus] PRIMARY KEY CLUSTERED ([ObligatoryID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_USR_FriendRequestStatus]
    ON [dbo].[USR_FriendRequestStatus]([ID] ASC);

