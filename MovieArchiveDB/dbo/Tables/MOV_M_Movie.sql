CREATE TABLE [dbo].[MOV_M_Movie] (
    [ID]                INT            IDENTITY (1, 1) NOT NULL,
    [Year]              INT            NULL,
    [ImdbID]            NVARCHAR (30)  NULL,
    [ImdbRating]        FLOAT (53)     NULL,
    [ImdbPoster]        NVARCHAR (500) NULL,
    [ImdbParsed]        BIT            NOT NULL,
    [ImdbLastParseDate] DATETIME       NULL,
    [InsertDate]        DATETIME       NOT NULL,
    [InsertUserID]      INT            CONSTRAINT [DF_M_Movie_InsertUserID] DEFAULT ((1)) NOT NULL,
    [UpdateDate]        DATETIME       NOT NULL,
    [UpdateUserID]      INT            CONSTRAINT [DF_M_Movie_UpdateUserID] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED ([ID] ASC)
);

