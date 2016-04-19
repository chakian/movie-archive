CREATE PROCEDURE [dbo].[ResetImdbId]
	@MovieID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE MOV_M_Movie
	SET Year=NULL, 
		ImdbID=NULL, 
		ImdbRating=NULL, 
		ImdbPoster=NULL, 
		ImdbParsed=0, 
		ImdbLastParseDate=NULL
	WHERE ID = @MovieID

	DELETE FROM MOV_M_Language WHERE MovieID = @MovieID

	DELETE FROM MOV_M_Actor WHERE MovieID = @MovieID

	DELETE FROM MOV_M_Director WHERE MovieID = @MovieID

	DELETE FROM MOV_M_Types WHERE MovieID = @MovieID

	DELETE FROM MOV_M_Writer WHERE MovieID = @MovieID

END