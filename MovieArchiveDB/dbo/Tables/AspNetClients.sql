CREATE TABLE [dbo].[AspNetClients] (
    [Id]               NVARCHAR (50)  NULL,
    [Secret]           NVARCHAR (250) NULL,
    [Name]             NVARCHAR (250) NULL,
    [ApplicationType]  INT            NULL,
    [Active]           BIT            NULL,
    [RefreshTokenLife] INT            NULL,
    [AllowedOrigin]    NVARCHAR (250) NULL
);

