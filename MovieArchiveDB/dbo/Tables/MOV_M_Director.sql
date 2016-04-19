CREATE TABLE [dbo].[MOV_M_Director] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [MovieID]    INT NOT NULL,
    [DirectorID] INT NOT NULL,
    CONSTRAINT [PK_Directors_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Directors_Directors1] FOREIGN KEY ([DirectorID]) REFERENCES [dbo].[INF_Director] ([ID]),
    CONSTRAINT [FK_Directors_Movies] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[MOV_M_Movie] ([ID]),
    CONSTRAINT [IX_Directors] UNIQUE NONCLUSTERED ([MovieID] ASC, [DirectorID] ASC)
);

