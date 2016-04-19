using System;
using System.IO;

namespace com.cagdaskorkut.utility {
	public class FileSystemOperations {
		/// <summary>
		/// Creates a directory and returns if the operation was successful
		/// </summary>
		/// <param name="path">Path to create</param>
		/// <returns></returns>
		public static void CreateDirectory ( string path ) {
			if ( !Directory.Exists ( path ) ) {
				try {
					Directory.CreateDirectory ( path );
				} catch ( Exception ex ) {
					throw new KnownException ( "CanNotCreateDirectory", ex );
				}
			}
		}

		/// <summary>
		/// Returns a valid file name which is not being used in the given path
		/// </summary>
		/// <param name="path">Full path which will be used to save the file</param>
		/// <param name="fileName">File name</param>
		/// <param name="extension">File extension</param>
		/// <returns></returns>
		public static string GetFileNameToPreventDuplicate ( string path, string fileName, string extension ) {
			string result = "";
			int v = 0;

			while ( File.Exists ( path + fileName + ( v != 0 ? v.ToString ( ) : "" ) + extension ) )
				v++;

			result = fileName + ( v != 0 ? v.ToString ( ) : "" ) + extension;

			return result;
		}
		/// <summary>
		/// Returns a valid file name which is not being used in the given path
		/// </summary>
		/// <param name="path">Full path</param>
		/// <param name="fileName">File name with extension</param>
		/// <returns></returns>
		public static string GetFileNameToPreventDuplicate ( string path, string fileName ) {
			string fileExtension = GetExtension ( fileName );
			if ( fileName.Contains ( "." ) )
				fileName = fileName.Remove ( fileName.LastIndexOf ( "." ) );

			return GetFileNameToPreventDuplicate ( path, fileName, fileExtension );
		}

		public static string GetExtension ( string input ) {
			if ( !String.IsNullOrEmpty ( input ) && input.Contains ( "." ) )
				return input.Substring ( input.LastIndexOf ( '.' ) );
			else
				return "";
		}
	}
}