CREATE TABLE [dbo].[MOV_M_UserRating] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [UserID]  INT NOT NULL,
    [MovieID] INT NOT NULL,
    [Watched] BIT NOT NULL,
    [Rating]  INT NULL,
    CONSTRAINT [PK_Ratings] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Ratings_Movies] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[MOV_M_Movie] ([ID]),
    CONSTRAINT [FK_Ratings_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [IX_Ratings] UNIQUE NONCLUSTERED ([UserID] ASC, [MovieID] ASC)
);

