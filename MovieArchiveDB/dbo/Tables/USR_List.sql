CREATE TABLE [dbo].[USR_List] (
    [ID]     INT            IDENTITY (1, 1) NOT NULL,
    [UserID] INT            NOT NULL,
    [Name]   NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_M_UserList] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_M_UserList_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

