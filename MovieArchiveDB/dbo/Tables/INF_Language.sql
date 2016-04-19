CREATE TABLE [dbo].[INF_Language] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED ([ID] ASC)
);

