CREATE TABLE [dbo].[USR_UserListAuthorization] (
    [ID]                  INT NOT NULL,
    [UserListID]          INT NOT NULL,
    [UserID]              INT NOT NULL,
    [AuthorizationTypeID] INT NOT NULL,
    CONSTRAINT [PK_USR_UserListAuthorization] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_USR_UserListAuthorization_MOV_M_UserList] FOREIGN KEY ([UserListID]) REFERENCES [dbo].[USR_List] ([ID]),
    CONSTRAINT [FK_USR_UserListAuthorization_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_USR_UserListAuthorization_USR_UserListAuthorizationType] FOREIGN KEY ([AuthorizationTypeID]) REFERENCES [dbo].[USR_UserListAuthorizationType] ([ID])
);



