



CREATE VIEW [dbo].[vMovieLanguage]
AS
SELECT ml.ID, ml.MovieID, ml.LanguageID, l.Name AS LanguageName
FROM MOV_M_Language AS ml INNER JOIN
	INF_Language AS l ON l.ID = ml.LanguageID