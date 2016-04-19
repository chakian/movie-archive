CREATE TABLE [dbo].[ZZ_M_Movie_History] (
    [HistoryID]         INT            IDENTITY (1, 1) NOT NULL,
    [ID]                INT            NOT NULL,
    [Year]              INT            NULL,
    [ImdbID]            NVARCHAR (30)  NULL,
    [ImdbRating]        FLOAT (53)     NULL,
    [ImdbPoster]        NVARCHAR (500) NULL,
    [ImdbParsed]        BIT            NOT NULL,
    [ImdbLastParseDate] DATETIME       NULL,
    [InsertDate]        DATETIME       NOT NULL,
    [InsertUserID]      INT            NOT NULL,
    [UpdateDate]        DATETIME       NOT NULL,
    [UpdateUserID]      INT            NOT NULL,
    CONSTRAINT [PK_M_Movie_History] PRIMARY KEY CLUSTERED ([HistoryID] ASC)
);

