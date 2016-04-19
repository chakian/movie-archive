



CREATE VIEW [dbo].[vMovieName]
AS
SELECT mn.ID, mn.MovieID, mn.LanguageID, mn.Name, mn.IsDefault, l.Name AS LanguageName
FROM MOV_M_Names AS mn INNER JOIN 
	INF_Language l ON l.ID = mn.LanguageID