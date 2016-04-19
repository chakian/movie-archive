CREATE TABLE [dbo].[INF_Director] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Directors] PRIMARY KEY CLUSTERED ([ID] ASC)
);

