﻿CREATE TABLE [dbo].[MOV_M_Types] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [MovieID] INT NOT NULL,
    [TypeID]  INT NOT NULL,
    CONSTRAINT [PK_Types_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Types_Movies] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[MOV_M_Movie] ([ID]),
    CONSTRAINT [FK_Types_Types1] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[INF_Type] ([ID]),
    CONSTRAINT [IX_Types] UNIQUE NONCLUSTERED ([MovieID] ASC, [TypeID] ASC)
);

