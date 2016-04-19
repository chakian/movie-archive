CREATE TABLE [dbo].[MOV_M_Actor] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [MovieID] INT NOT NULL,
    [ActorID] INT NOT NULL,
    CONSTRAINT [PK_Actors_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Actors_Actors1] FOREIGN KEY ([ActorID]) REFERENCES [dbo].[INF_Actor] ([ID]),
    CONSTRAINT [FK_Actors_Movies] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[MOV_M_Movie] ([ID]),
    CONSTRAINT [IX_Actors] UNIQUE NONCLUSTERED ([MovieID] ASC, [ActorID] ASC)
);

