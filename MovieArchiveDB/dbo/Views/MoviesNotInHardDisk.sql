
CREATE VIEW [dbo].[MoviesNotInHardDisk] AS
SELECT n.Name, m.* FROM MOV_M_Movie m, MOV_M_Names n
WHERE m.ID NOT IN (SELECT MovieID FROM MOV_M_Archive)
	AND m.ID = n.MovieID AND n.IsDefault=1