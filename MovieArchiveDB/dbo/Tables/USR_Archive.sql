CREATE TABLE [dbo].[USR_Archive] (
    [ID]     INT            IDENTITY (1, 1) NOT NULL,
    [UserID] INT            NOT NULL,
    [Name]   NVARCHAR (100) NOT NULL,
    [Path]   NVARCHAR (100) NULL,
    CONSTRAINT [PK_Archives] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_USR_Archive_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

