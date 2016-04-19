using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MArchiveLibrary;

namespace MArchiveImdbParser {
	public partial class imdbInfoForm : Form {
		imdbModel iinfo;
		string senderName;
		object senderForm;

		public imdbInfoForm ( ) {
			InitializeComponent ( );
		}
		public imdbInfoForm ( imdbModel imdbInfo, object senderF, string senderN ) {
			InitializeComponent ( );
			iinfo = imdbInfo;
			senderForm = senderF;
			senderName = senderN;
		}

		#region Director Buttons
		private void btnAddDirector_Click ( object sender, EventArgs e ) {
			lstDirector.Items.Add ( cmbDirector.Text );
			cmbDirector.Text = "";
		}
		private void btnDelDirector_Click ( object sender, EventArgs e ) {
			if ( lstDirector.SelectedIndex > -1 ) {
				lstDirector.Items.RemoveAt ( lstDirector.SelectedIndex );
			}
		}
		private void cmbDirector_Enter ( object sender, EventArgs e ) {
			this.AcceptButton = btnAddDirector;
		}
		private void cmbDirector_Leave ( object sender, EventArgs e ) {
			this.AcceptButton = null;
		}
		#endregion

		#region Cast Buttons
		private void btnAddCast_Click ( object sender, EventArgs e ) {
			lstCast.Items.Add ( cmbCast.Text );
			cmbCast.Text = "";
		}
		private void btnDelCast_Click ( object sender, EventArgs e ) {
			if ( lstCast.SelectedIndex > -1 ) {
				lstCast.Items.RemoveAt ( lstCast.SelectedIndex );
			}
		}
		private void cmbCast_Enter ( object sender, EventArgs e ) {
			this.AcceptButton = btnAddCast;
		}
		private void cmbCast_Leave ( object sender, EventArgs e ) {
			this.AcceptButton = null;
		}
		#endregion

		#region Writer Buttons
		private void btnAddWriter_Click ( object sender, EventArgs e ) {
			lstWriter.Items.Add ( cmbWriter.Text );
			cmbWriter.Text = "";
		}
		private void btnDelWriter_Click ( object sender, EventArgs e ) {
			if ( lstWriter.SelectedIndex > -1 ) {
				lstWriter.Items.RemoveAt ( lstWriter.SelectedIndex );
			}
		}
		private void cmbWriter_Enter ( object sender, EventArgs e ) {
			this.AcceptButton = btnAddWriter;
		}
		private void cmbWriter_Leave ( object sender, EventArgs e ) {
			this.AcceptButton = null;
		}
		#endregion

		#region Type Buttons
		private void btnAddType_Click ( object sender, EventArgs e ) {
			lstType.Items.Add ( cmbType.Text );
			cmbType.Text = "";
		}
		private void btnDelType_Click ( object sender, EventArgs e ) {
			if ( lstType.SelectedIndex > -1 ) {
				lstType.Items.RemoveAt ( lstType.SelectedIndex );
			}
		}
		private void cmbType_Enter ( object sender, EventArgs e ) {
			this.AcceptButton = btnAddType;
		}
		private void cmbType_Leave ( object sender, EventArgs e ) {
			this.AcceptButton = null;
		}
		#endregion

		#region Language Buttons
		private void btnAddLanguage_Click ( object sender, EventArgs e ) {
			lstLanguage.Items.Add ( cmbLanguage.Text );
			cmbLanguage.Text = "";
		}
		private void btnDelLanguage_Click ( object sender, EventArgs e ) {
			if ( lstLanguage.SelectedIndex > -1 ) {
				lstLanguage.Items.RemoveAt ( lstLanguage.SelectedIndex );
			}
		}
		private void cmbLanguage_Enter ( object sender, EventArgs e ) {
			this.AcceptButton = btnAddLanguage;
		}
		private void cmbLanguage_Leave ( object sender, EventArgs e ) {
			this.AcceptButton = null;
		}
		#endregion

		private void imdbInfoForm_Load ( object sender, EventArgs e ) {
			if ( iinfo.picturePath != "" ) {
				picImdb.ImageLocation = iinfo.picturePath;
			}
			txtRating.Text = iinfo.imdbRating.ToString ( );
			txtYear.Text = iinfo.year.ToString ( );
			//director
			if ( iinfo.directors != null )
				foreach ( string t in iinfo.directors ) {
					lstDirector.Items.Add ( t );
				}
			//writer
			if ( iinfo.writers != null )
				foreach ( string t in iinfo.writers ) {
					lstWriter.Items.Add ( t );
				}
			//type
			if ( iinfo.genres != null )
				foreach ( string t in iinfo.genres ) {
					lstType.Items.Add ( t );
				}
			//cast
			if ( iinfo.cast != null )
				foreach ( string t in iinfo.cast ) {
					lstCast.Items.Add ( t );
				}
			//language
			if ( iinfo.languages != null )
				foreach ( string t in iinfo.languages ) {
					lstLanguage.Items.Add ( t );
				}
		}

		private void btnSave_Click ( object sender, EventArgs e ) {
			imdbModel temp = new imdbModel ( );

			//ID
			temp.id = iinfo.id;
			//Year
			if ( txtYear.Text != "" )
				temp.year = ( chkYear.Checked ? int.Parse ( txtYear.Text ) : 0 );
			else
				temp.year = 0;
			//Rating
			if ( txtRating.Text != "" )
				temp.imdbRating = ( chkRating.Checked ? double.Parse ( txtRating.Text ) : 0 );
			else
				temp.imdbRating = 0;
			//Picture
			temp.picturePath = ( chkPicture.Checked ? iinfo.picturePath : "" );
			//genre
			if ( chkType.Checked ) {
				temp.genres = new List<string> ( );
				for ( int i = 0; i < lstType.Items.Count; i++ ) {
					temp.genres.Add ( lstType.Items[i].ToString ( ) );
				}
			}
			//cast
			if ( chkCast.Checked ) {
				temp.cast = new List<string> ( );
				for ( int i = 0; i < lstCast.Items.Count; i++ ) {
					temp.cast.Add ( lstCast.Items[i].ToString ( ) );
				}
			}
			//director
			if ( chkDirector.Checked ) {
				temp.directors = new List<string> ( );
				for ( int i = 0; i < lstDirector.Items.Count; i++ ) {
					temp.directors.Add ( lstDirector.Items[i].ToString ( ) );
				}
			}
			//writer
			if ( chkWriter.Checked ) {
				temp.writers = new List<string> ( );
				for ( int i = 0; i < lstWriter.Items.Count; i++ ) {
					temp.writers.Add ( lstWriter.Items[i].ToString ( ) );
				}
			}
			//language
			if ( chkLanguage.Checked ) {
				temp.languages = new List<string> ( );
				for ( int i = 0; i < lstLanguage.Items.Count; i++ ) {
					temp.languages.Add ( lstLanguage.Items[i].ToString ( ) );
				}
			}

			//post save operations
			//switch (senderName)
			//{
			//    case "addnew":
			//        addnew frmSender = (addnew)senderForm;
			//        frmSender.fillInformationFromImdb(temp);
			//        break;
			//    case "edit":
			//        edit frmSender2 = (edit)senderForm;
			//        frmSender2.fillInformationFromImdb(temp);
			//        break;
			//}
			this.Close ( );
		}

		private void lstCast_KeyDown ( object sender, KeyEventArgs e ) {
			if ( e.KeyCode == Keys.Delete ) {
				btnDelCast_Click ( sender, e );
			}
		}

		private void btnSelectAll_Click ( object sender, EventArgs e ) {
			chkCast.Checked = true;
			chkDirector.Checked = true;
			chkLanguage.Checked = true;
			chkPicture.Checked = true;
			chkRating.Checked = true;
			chkType.Checked = true;
			chkWriter.Checked = true;
			chkYear.Checked = true;
		}

		private void btnSelectNone_Click ( object sender, EventArgs e ) {
			chkCast.Checked = false;
			chkDirector.Checked = false;
			chkLanguage.Checked = false;
			chkPicture.Checked = false;
			chkRating.Checked = false;
			chkType.Checked = false;
			chkWriter.Checked = false;
			chkYear.Checked = false;
		}
	}
}