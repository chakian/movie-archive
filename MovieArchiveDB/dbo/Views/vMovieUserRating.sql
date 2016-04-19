



CREATE VIEW [dbo].[vMovieUserRating]
AS
SELECT mur.ID, mur.MovieID, mur.UserID, mur.Watched, mur.Rating, u.Name AS UserName
FROM MOV_M_UserRating AS mur INNER JOIN 
	dbo.[User] u ON u.ID = mur.UserID