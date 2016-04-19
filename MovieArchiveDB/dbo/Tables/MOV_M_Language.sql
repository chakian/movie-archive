CREATE TABLE [dbo].[MOV_M_Language] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [MovieID]    INT NOT NULL,
    [LanguageID] INT NOT NULL,
    CONSTRAINT [PK_Languages_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Languages_Languages1] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[INF_Language] ([ID]),
    CONSTRAINT [FK_Languages_Movies] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[MOV_M_Movie] ([ID]),
    CONSTRAINT [IX_Languages] UNIQUE NONCLUSTERED ([MovieID] ASC, [LanguageID] ASC)
);

