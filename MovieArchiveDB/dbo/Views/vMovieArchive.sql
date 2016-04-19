



CREATE VIEW [dbo].[vMovieArchive]
AS
SELECT     ma.ID, ma.MovieID, a.UserID, ma.ArchiveID, ma.Resolution, ma.FileExtension, ma.Path, a.Name AS ArchiveName, a.Path AS ArchivePath
FROM         MOV_M_Archive AS ma INNER JOIN
                      USR_Archive AS a ON a.ID = ma.ArchiveID