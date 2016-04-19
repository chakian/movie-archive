CREATE TABLE [dbo].[MOV_M_Names] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [MovieID]    INT            NOT NULL,
    [LanguageID] INT            NOT NULL,
    [Name]       NVARCHAR (250) NOT NULL,
    [IsDefault]  BIT            NOT NULL,
    CONSTRAINT [PK_MovieNames] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_MovieNames_Languages] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[INF_Language] ([ID]),
    CONSTRAINT [FK_MovieNames_Movies] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[MOV_M_Movie] ([ID]),
    CONSTRAINT [IX_MovieNames] UNIQUE NONCLUSTERED ([MovieID] ASC, [LanguageID] ASC)
);

