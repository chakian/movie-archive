



CREATE VIEW [dbo].[vMovieDirector]
AS
SELECT md.ID, md.MovieID, md.DirectorID, d.Name AS DirectorName
FROM MOV_M_Director AS md INNER JOIN
	INF_Director AS d ON d.ID = md.DirectorID