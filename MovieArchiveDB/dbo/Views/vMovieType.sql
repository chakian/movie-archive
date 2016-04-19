




CREATE VIEW [dbo].[vMovieType]
AS
SELECT mt.ID, mt.MovieID, t.ID AS TypeID, t.Name AS TypeName
FROM MOV_M_Types AS mt INNER JOIN 
	INF_Type t ON t.ID = mt.TypeID