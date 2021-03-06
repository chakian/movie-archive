/****** Object:  Table [dbo].[USR_UserListAuthorizationType]    Script Date: 08/31/2015 14:00:26 ******/
INSERT INTO USR_UserListAuthorizationType (ID, Name) VALUES (1, 'Owner')
INSERT INTO USR_UserListAuthorizationType (ID, Name) VALUES (2, 'Admin')
INSERT INTO USR_UserListAuthorizationType (ID, Name) VALUES (3, 'Editor')
INSERT INTO USR_UserListAuthorizationType (ID, Name) VALUES (4, 'Viewer')
INSERT INTO USR_UserListAuthorizationType (ID, Name) VALUES (5, 'Blocked')

/*USR_UserListAuthorization*/
DECLARE @ListID int
DECLARE @UserID int

DECLARE userlistCursor CURSOR
FOR SELECT ID, UserID FROM MOV_M_UserList

OPEN userlistCursor
IF @@CURSOR_ROWS > 0
BEGIN
	FETCH NEXT FROM userlistCursor INTO @ListID, @UserID
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO USR_UserListAuthorization (UserListID, UserID, AuthorizationTypeID) VALUES (@ListID, @UserID, 1 /*Owner*/)
		FETCH NEXT FROM userlistCursor INTO @ListID, @UserID
	END
END

CLOSE userlistCursor
