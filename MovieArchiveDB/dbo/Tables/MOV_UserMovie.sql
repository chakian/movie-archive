CREATE TABLE [dbo].[MOV_UserMovie] (
    [ID]           INT      IDENTITY (1, 1) NOT NULL,
    [UserID]       INT      NOT NULL,
    [MovieID]      INT      NOT NULL,
    [InsertDate]   DATETIME NOT NULL,
    [InsertUserID] INT      NOT NULL,
    [UpdateDate]   DATETIME NOT NULL,
    [UpdateUserID] INT      NOT NULL,
    CONSTRAINT [PK_UserMovie] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserMovie_M_Movie] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[MOV_M_Movie] ([ID]),
    CONSTRAINT [FK_UserMovie_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

