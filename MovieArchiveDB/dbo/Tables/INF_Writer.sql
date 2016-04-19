CREATE TABLE [dbo].[INF_Writer] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Writers] PRIMARY KEY CLUSTERED ([ID] ASC)
);

