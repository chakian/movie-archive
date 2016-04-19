

CREATE VIEW [dbo].[vUserListMoviesDetail] AS

SELECT l.UserID, l.ID AS ListID, l.Name AS ListName, lm.ID AS ListMovieID, lm.MovieID AS MovieID, n.Name AS MovieName, lm.SortOrder, lm.IsChecked, r.Watched
FROM USR_List l
	LEFT OUTER JOIN USR_ListMovie lm ON l.ID = lm.ListID
	LEFT OUTER JOIN MOV_M_Names n ON n.IsDefault = 1 AND lm.MovieID = n.MovieID
	LEFT OUTER JOIN MOV_M_UserRating r ON r.MovieID = lm.MovieID AND r.UserID = l.UserID