using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MArchiveLibrary;

namespace TestWinForm {
	public partial class bulkHop : Form {
		public bulkHop ( ) {
			InitializeComponent ( );
		}

		private void btnSave_Click ( object sender, EventArgs e ) {
			int counter = 0;
			//get the movie name list
			string[] idArr = System.Text.RegularExpressions.Regex.Split ( txtId.Text, "\r\n" );
			string[] pathArr = System.Text.RegularExpressions.Regex.Split ( txtPath.Text, "\r\n" );
			string[] resArr = System.Text.RegularExpressions.Regex.Split ( txtResExt.Text, "\r\n" );
			for(int i=0;i<idArr.Length;i++){
				int movieId = validationHelper.getInt32 ( idArr[i], 0 );
				if ( movieId != 0 ) {
					string path = pathArr[i].Replace ( "M:\\", "" );
					string[] resExt = resArr[i].Split ( ',' );
					string resolution = resExt[0];
					string extension = resExt[1];

					tblMovieArchive newMovArc = new tblMovieArchive ( );
					newMovArc.archiveId = 3;
					newMovArc.movieId = movieId;
					newMovArc.resolution = resolution;
					newMovArc.fileExtension = extension;
					newMovArc.path = path;

					archiveData aData = new archiveData ( );
					aData.insertMovieArchive ( newMovArc );

					counter++;
				}
			}

			MessageBox.Show ( counter.ToString ( ) + " movies successfully added" );
		}

		private void bulkHop_Load ( object sender, EventArgs e ) {
			//string test = "C:\\Windows\\Meraba\\Evet";
			//int startingIndex = test.IndexOf ( ":\\" );
			//string test2 = test.Substring ( startingIndex );
			//MessageBox.Show ( test2 );
		}
	}
}
