CREATE TABLE [dbo].[AspNetRefreshTokens] (
    [Id]              NVARCHAR (250) NULL,
    [Subject]         NVARCHAR (250) NULL,
    [ClientId]        NCHAR (50)     NULL,
    [IssuedUtc]       DATETIME       NULL,
    [ExpiresUtc]      DATETIME       NULL,
    [ProtectedTicket] NVARCHAR (MAX) NULL
);

