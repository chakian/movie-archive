CREATE TABLE [dbo].[MOV_M_Writer] (
    [ID]       INT IDENTITY (1, 1) NOT NULL,
    [MovieID]  INT NOT NULL,
    [WriterID] INT NOT NULL,
    CONSTRAINT [PK_Writers_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Writers_Movies] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[MOV_M_Movie] ([ID]),
    CONSTRAINT [FK_Writers_Writers] FOREIGN KEY ([WriterID]) REFERENCES [dbo].[INF_Writer] ([ID]),
    CONSTRAINT [IX_Writers] UNIQUE NONCLUSTERED ([MovieID] ASC, [WriterID] ASC)
);

