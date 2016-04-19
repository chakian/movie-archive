CREATE TABLE [dbo].[USR_UserListAuthorizationType] (
    [RequiredPKID] INT           IDENTITY (1, 1) NOT NULL,
    [ID]           INT           NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_USR_UserListAuthorizationType] PRIMARY KEY CLUSTERED ([RequiredPKID] ASC),
    CONSTRAINT [IX_USR_UserListAuthorizationType] UNIQUE NONCLUSTERED ([ID] ASC)
);

