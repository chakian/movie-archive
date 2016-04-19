CREATE TABLE [dbo].[User] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [Username] NVARCHAR (50)  NOT NULL,
    [Password] NVARCHAR (100) NOT NULL,
    [Name]     NVARCHAR (100) NULL,
    [Email]    NVARCHAR (250) NULL,
    [IsAdmin]  BIT            NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [IX_User_Username] UNIQUE NONCLUSTERED ([Username] ASC)
);



