CREATE TABLE [dbo].[MOV_M_Archive] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [MovieID]       INT             NOT NULL,
    [ArchiveID]     INT             NOT NULL,
    [Resolution]    NVARCHAR (30)   NULL,
    [FileExtension] NVARCHAR (10)   NULL,
    [Path]          NVARCHAR (1000) NULL,
    CONSTRAINT [PK_Archives_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Archives_Archives1] FOREIGN KEY ([ArchiveID]) REFERENCES [dbo].[USR_Archive] ([ID]),
    CONSTRAINT [FK_Archives_Movies] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[MOV_M_Movie] ([ID])
);



