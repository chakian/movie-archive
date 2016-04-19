CREATE TABLE [dbo].[USR_ListMovie] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [ListID]    INT NOT NULL,
    [MovieID]   INT NOT NULL,
    [SortOrder] INT NOT NULL,
    [IsChecked] BIT CONSTRAINT [DF_M_UserListMovie_IsChecked] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_M_UserListMovies] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_M_UserListMovies_M_Movie] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[MOV_M_Movie] ([ID]),
    CONSTRAINT [FK_M_UserListMovies_M_UserList] FOREIGN KEY ([ListID]) REFERENCES [dbo].[USR_List] ([ID]),
    CONSTRAINT [IX_M_UserListMovie] UNIQUE NONCLUSTERED ([ListID] ASC, [MovieID] ASC)
);

