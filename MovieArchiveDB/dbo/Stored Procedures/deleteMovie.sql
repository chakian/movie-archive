CREATE PROCEDURE [dbo].[deleteMovie]
	@movieId int
AS
BEGIN
	DELETE FROM MOV_M_Actor WHERE MovieID=@movieId
	
	DELETE FROM MOV_M_Writer WHERE MovieID=@movieId
	
	DELETE FROM MOV_M_Director WHERE MovieID=@movieId
	
	DELETE FROM MOV_M_Archive WHERE MovieID=@movieId
	
	DELETE FROM MOV_M_Types WHERE MovieID=@movieId
	
	DELETE FROM MOV_M_Language WHERE MovieID=@movieId
	
	DELETE FROM MOV_M_UserRating WHERE MovieID=@movieId
	
	DELETE FROM MOV_M_Names WHERE MovieID=@movieId
	
	DELETE FROM MOV_M_Movie WHERE ID=@movieId
END