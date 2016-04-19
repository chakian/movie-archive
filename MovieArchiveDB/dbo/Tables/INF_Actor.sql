CREATE TABLE [dbo].[INF_Actor] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Actors] PRIMARY KEY CLUSTERED ([ID] ASC)
);

