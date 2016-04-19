namespace MArchiveImdbParser
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.btnGetByImdbId = new System.Windows.Forms.Button();
			this.txtImdbId = new System.Windows.Forms.TextBox();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.btnSearch = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.parseStatus = new System.Windows.Forms.ProgressBar();
			this.lblProgress = new System.Windows.Forms.Label();
			this.chkCheckPosterless = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// btnGetByImdbId
			// 
			this.btnGetByImdbId.Location = new System.Drawing.Point(12, 38);
			this.btnGetByImdbId.Name = "btnGetByImdbId";
			this.btnGetByImdbId.Size = new System.Drawing.Size(100, 44);
			this.btnGetByImdbId.TabIndex = 0;
			this.btnGetByImdbId.Text = "Get Selected IMDB ID";
			this.btnGetByImdbId.UseVisualStyleBackColor = true;
			this.btnGetByImdbId.Click += new System.EventHandler(this.btnGetByImdbId_Click);
			// 
			// txtImdbId
			// 
			this.txtImdbId.Location = new System.Drawing.Point(12, 12);
			this.txtImdbId.Name = "txtImdbId";
			this.txtImdbId.Size = new System.Drawing.Size(100, 20);
			this.txtImdbId.TabIndex = 1;
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(556, 12);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(100, 20);
			this.txtSearch.TabIndex = 2;
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(567, 38);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(75, 23);
			this.btnSearch.TabIndex = 3;
			this.btnSearch.Text = "Search ( )";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// btnStart
			// 
			this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.btnStart.Location = new System.Drawing.Point(65, 165);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(542, 239);
			this.btnStart.TabIndex = 4;
			this.btnStart.Text = "Start !";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// parseStatus
			// 
			this.parseStatus.Location = new System.Drawing.Point(65, 410);
			this.parseStatus.Maximum = 1;
			this.parseStatus.Name = "parseStatus";
			this.parseStatus.Size = new System.Drawing.Size(542, 69);
			this.parseStatus.TabIndex = 5;
			// 
			// lblProgress
			// 
			this.lblProgress.BackColor = System.Drawing.Color.Transparent;
			this.lblProgress.Location = new System.Drawing.Point(62, 495);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(542, 23);
			this.lblProgress.TabIndex = 6;
			this.lblProgress.Text = "Progress";
			this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// chkCheckPosterless
			// 
			this.chkCheckPosterless.AutoSize = true;
			this.chkCheckPosterless.Location = new System.Drawing.Point(65, 142);
			this.chkCheckPosterless.Name = "chkCheckPosterless";
			this.chkCheckPosterless.Size = new System.Drawing.Size(247, 17);
			this.chkCheckPosterless.TabIndex = 7;
			this.chkCheckPosterless.Text = "Check Imdb Parsed but posterless movies too?";
			this.chkCheckPosterless.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(688, 544);
			this.Controls.Add(this.chkCheckPosterless);
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.parseStatus);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.txtSearch);
			this.Controls.Add(this.txtImdbId);
			this.Controls.Add(this.btnGetByImdbId);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetByImdbId;
        private System.Windows.Forms.TextBox txtImdbId;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.ProgressBar parseStatus;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.CheckBox chkCheckPosterless;
    }
}

