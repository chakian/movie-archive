using System;
using System.IO;

namespace MArchiveLibrary.Helpers
{
	public class FileSystemHelper {
		public static string getMoviePictureSavePath ( ) {//string basePath ) {
			//String savePath = basePath + staticResources.moviePicturePath;
			String savePath = staticResources.path_fullWebSiteImagePath;
			if ( !Directory.Exists ( savePath ) )
				Directory.CreateDirectory ( savePath );

			return savePath + "\\";
		}
		public static string getMoviePictureShowPath ( ) {
			return staticResources.url_fullWebSiteImageUrl + "/";
		}
		public static string prepareFileNameForPicture ( string originalFileName, MovieNameModel mov, string savePath ) {
			string fileNameReturn, extension;
			// create file name
			fileNameReturn = mov.OriginalName;

            fileNameReturn = fileNameReturn
                .Replace("\\", "_")
                .Replace("/", "_")
                .Replace(":", "_")
                .Replace("*", "_")
                .Replace("?", "_")
                .Replace("\"", "_")
                .Replace("<", "_")
                .Replace(">", "_")
                .Replace("|", "_")
                .Replace("'", "_")
                .Replace(" ", "_")
                .Replace("&", "_");

			// decide for the extension
			if ( !String.IsNullOrEmpty ( originalFileName ) )
				extension = originalFileName.Substring ( originalFileName.LastIndexOf ( '.' ) );
			else
				extension = ".jpg";

			if ( File.Exists ( savePath + fileNameReturn + extension ) ) {
				int i = 0;
				do {
					fileNameReturn = fileNameReturn + i.ToString ( );
					i++;
				} while ( File.Exists ( savePath + fileNameReturn + extension ) );
			}

			return fileNameReturn + extension;
		}
	}
}