



CREATE VIEW [dbo].[vMovieWriter]
AS
SELECT mw.ID, mw.MovieID, mw.WriterID, w.Name AS WriterName
FROM MOV_M_Writer AS mw INNER JOIN 
	INF_Writer w ON w.ID = mw.WriterID