namespace MArchiveImdbParser
{
    partial class imdbInfoForm
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
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.chkLanguage = new System.Windows.Forms.CheckBox();
            this.chkCast = new System.Windows.Forms.CheckBox();
            this.chkType = new System.Windows.Forms.CheckBox();
            this.chkWriter = new System.Windows.Forms.CheckBox();
            this.chkDirector = new System.Windows.Forms.CheckBox();
            this.chkYear = new System.Windows.Forms.CheckBox();
            this.chkRating = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.btnDelLanguage = new System.Windows.Forms.Button();
            this.lstLanguage = new System.Windows.Forms.ListBox();
            this.btnAddLanguage = new System.Windows.Forms.Button();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.lblWriter = new System.Windows.Forms.Label();
            this.lblDirector = new System.Windows.Forms.Label();
            this.btnDelWriter = new System.Windows.Forms.Button();
            this.lstWriter = new System.Windows.Forms.ListBox();
            this.btnAddWriter = new System.Windows.Forms.Button();
            this.cmbWriter = new System.Windows.Forms.ComboBox();
            this.btnDelDirector = new System.Windows.Forms.Button();
            this.lstDirector = new System.Windows.Forms.ListBox();
            this.btnAddDirector = new System.Windows.Forms.Button();
            this.cmbDirector = new System.Windows.Forms.ComboBox();
            this.btnDelCast = new System.Windows.Forms.Button();
            this.lstCast = new System.Windows.Forms.ListBox();
            this.btnAddCast = new System.Windows.Forms.Button();
            this.cmbCast = new System.Windows.Forms.ComboBox();
            this.lblCast = new System.Windows.Forms.Label();
            this.btnDelType = new System.Windows.Forms.Button();
            this.btnAddType = new System.Windows.Forms.Button();
            this.lstType = new System.Windows.Forms.ListBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.chkPicture = new System.Windows.Forms.CheckBox();
            this.picImdb = new System.Windows.Forms.PictureBox();
            this.lblRating = new System.Windows.Forms.Label();
            this.txtRating = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picImdb)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(103, 537);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 131;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Location = new System.Drawing.Point(22, 537);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(75, 23);
            this.btnSelectNone.TabIndex = 130;
            this.btnSelectNone.Text = "Select None";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // chkLanguage
            // 
            this.chkLanguage.AutoSize = true;
            this.chkLanguage.Location = new System.Drawing.Point(392, 405);
            this.chkLanguage.Name = "chkLanguage";
            this.chkLanguage.Size = new System.Drawing.Size(41, 17);
            this.chkLanguage.TabIndex = 129;
            this.chkLanguage.Text = "OK";
            this.chkLanguage.UseVisualStyleBackColor = true;
            // 
            // chkCast
            // 
            this.chkCast.AutoSize = true;
            this.chkCast.Location = new System.Drawing.Point(368, 212);
            this.chkCast.Name = "chkCast";
            this.chkCast.Size = new System.Drawing.Size(41, 17);
            this.chkCast.TabIndex = 128;
            this.chkCast.Text = "OK";
            this.chkCast.UseVisualStyleBackColor = true;
            // 
            // chkType
            // 
            this.chkType.AutoSize = true;
            this.chkType.Location = new System.Drawing.Point(368, 9);
            this.chkType.Name = "chkType";
            this.chkType.Size = new System.Drawing.Size(41, 17);
            this.chkType.TabIndex = 127;
            this.chkType.Text = "OK";
            this.chkType.UseVisualStyleBackColor = true;
            // 
            // chkWriter
            // 
            this.chkWriter.AutoSize = true;
            this.chkWriter.Location = new System.Drawing.Point(88, 404);
            this.chkWriter.Name = "chkWriter";
            this.chkWriter.Size = new System.Drawing.Size(41, 17);
            this.chkWriter.TabIndex = 126;
            this.chkWriter.Text = "OK";
            this.chkWriter.UseVisualStyleBackColor = true;
            // 
            // chkDirector
            // 
            this.chkDirector.AutoSize = true;
            this.chkDirector.Location = new System.Drawing.Point(97, 305);
            this.chkDirector.Name = "chkDirector";
            this.chkDirector.Size = new System.Drawing.Size(41, 17);
            this.chkDirector.TabIndex = 125;
            this.chkDirector.Text = "OK";
            this.chkDirector.UseVisualStyleBackColor = true;
            // 
            // chkYear
            // 
            this.chkYear.AutoSize = true;
            this.chkYear.Location = new System.Drawing.Point(174, 268);
            this.chkYear.Name = "chkYear";
            this.chkYear.Size = new System.Drawing.Size(41, 17);
            this.chkYear.TabIndex = 124;
            this.chkYear.Text = "OK";
            this.chkYear.UseVisualStyleBackColor = true;
            // 
            // chkRating
            // 
            this.chkRating.AutoSize = true;
            this.chkRating.Location = new System.Drawing.Point(174, 229);
            this.chkRating.Name = "chkRating";
            this.chkRating.Size = new System.Drawing.Size(41, 17);
            this.chkRating.TabIndex = 123;
            this.chkRating.Text = "OK";
            this.chkRating.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(191, 537);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(364, 23);
            this.btnSave.TabIndex = 122;
            this.btnSave.Text = "Save It";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(331, 405);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(55, 13);
            this.lblLanguage.TabIndex = 121;
            this.lblLanguage.Text = "Language";
            // 
            // btnDelLanguage
            // 
            this.btnDelLanguage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDelLanguage.Location = new System.Drawing.Point(304, 492);
            this.btnDelLanguage.Name = "btnDelLanguage";
            this.btnDelLanguage.Size = new System.Drawing.Size(24, 23);
            this.btnDelLanguage.TabIndex = 120;
            this.btnDelLanguage.TabStop = false;
            this.btnDelLanguage.Text = "-";
            this.btnDelLanguage.UseVisualStyleBackColor = true;
            this.btnDelLanguage.Click += new System.EventHandler(this.btnDelLanguage_Click);
            // 
            // lstLanguage
            // 
            this.lstLanguage.FormattingEnabled = true;
            this.lstLanguage.Location = new System.Drawing.Point(334, 459);
            this.lstLanguage.Name = "lstLanguage";
            this.lstLanguage.Size = new System.Drawing.Size(221, 56);
            this.lstLanguage.TabIndex = 119;
            this.lstLanguage.TabStop = false;
            // 
            // btnAddLanguage
            // 
            this.btnAddLanguage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAddLanguage.Location = new System.Drawing.Point(532, 430);
            this.btnAddLanguage.Name = "btnAddLanguage";
            this.btnAddLanguage.Size = new System.Drawing.Size(23, 23);
            this.btnAddLanguage.TabIndex = 118;
            this.btnAddLanguage.TabStop = false;
            this.btnAddLanguage.Text = "+";
            this.btnAddLanguage.UseVisualStyleBackColor = true;
            this.btnAddLanguage.Click += new System.EventHandler(this.btnAddLanguage_Click);
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(334, 430);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(191, 21);
            this.cmbLanguage.TabIndex = 117;
            this.cmbLanguage.Enter += new System.EventHandler(this.cmbLanguage_Enter);
            this.cmbLanguage.Leave += new System.EventHandler(this.cmbLanguage_Leave);
            // 
            // lblWriter
            // 
            this.lblWriter.AutoSize = true;
            this.lblWriter.Location = new System.Drawing.Point(47, 405);
            this.lblWriter.Name = "lblWriter";
            this.lblWriter.Size = new System.Drawing.Size(35, 13);
            this.lblWriter.TabIndex = 116;
            this.lblWriter.Text = "Writer";
            // 
            // lblDirector
            // 
            this.lblDirector.AutoSize = true;
            this.lblDirector.Location = new System.Drawing.Point(47, 305);
            this.lblDirector.Name = "lblDirector";
            this.lblDirector.Size = new System.Drawing.Size(44, 13);
            this.lblDirector.TabIndex = 115;
            this.lblDirector.Text = "Director";
            // 
            // btnDelWriter
            // 
            this.btnDelWriter.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDelWriter.Location = new System.Drawing.Point(20, 492);
            this.btnDelWriter.Name = "btnDelWriter";
            this.btnDelWriter.Size = new System.Drawing.Size(24, 23);
            this.btnDelWriter.TabIndex = 114;
            this.btnDelWriter.TabStop = false;
            this.btnDelWriter.Text = "-";
            this.btnDelWriter.UseVisualStyleBackColor = true;
            this.btnDelWriter.Click += new System.EventHandler(this.btnDelWriter_Click);
            // 
            // lstWriter
            // 
            this.lstWriter.FormattingEnabled = true;
            this.lstWriter.Location = new System.Drawing.Point(50, 459);
            this.lstWriter.Name = "lstWriter";
            this.lstWriter.Size = new System.Drawing.Size(221, 56);
            this.lstWriter.TabIndex = 113;
            this.lstWriter.TabStop = false;
            // 
            // btnAddWriter
            // 
            this.btnAddWriter.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAddWriter.Location = new System.Drawing.Point(248, 430);
            this.btnAddWriter.Name = "btnAddWriter";
            this.btnAddWriter.Size = new System.Drawing.Size(23, 23);
            this.btnAddWriter.TabIndex = 112;
            this.btnAddWriter.TabStop = false;
            this.btnAddWriter.Text = "+";
            this.btnAddWriter.UseVisualStyleBackColor = true;
            this.btnAddWriter.Click += new System.EventHandler(this.btnAddWriter_Click);
            // 
            // cmbWriter
            // 
            this.cmbWriter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbWriter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbWriter.FormattingEnabled = true;
            this.cmbWriter.Location = new System.Drawing.Point(50, 430);
            this.cmbWriter.Name = "cmbWriter";
            this.cmbWriter.Size = new System.Drawing.Size(191, 21);
            this.cmbWriter.TabIndex = 108;
            this.cmbWriter.Enter += new System.EventHandler(this.cmbWriter_Enter);
            this.cmbWriter.Leave += new System.EventHandler(this.cmbWriter_Leave);
            // 
            // btnDelDirector
            // 
            this.btnDelDirector.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDelDirector.Location = new System.Drawing.Point(20, 379);
            this.btnDelDirector.Name = "btnDelDirector";
            this.btnDelDirector.Size = new System.Drawing.Size(24, 23);
            this.btnDelDirector.TabIndex = 111;
            this.btnDelDirector.TabStop = false;
            this.btnDelDirector.Text = "-";
            this.btnDelDirector.UseVisualStyleBackColor = true;
            this.btnDelDirector.Click += new System.EventHandler(this.btnDelDirector_Click);
            // 
            // lstDirector
            // 
            this.lstDirector.FormattingEnabled = true;
            this.lstDirector.Location = new System.Drawing.Point(50, 359);
            this.lstDirector.Name = "lstDirector";
            this.lstDirector.Size = new System.Drawing.Size(221, 43);
            this.lstDirector.TabIndex = 110;
            this.lstDirector.TabStop = false;
            // 
            // btnAddDirector
            // 
            this.btnAddDirector.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAddDirector.Location = new System.Drawing.Point(248, 330);
            this.btnAddDirector.Name = "btnAddDirector";
            this.btnAddDirector.Size = new System.Drawing.Size(23, 23);
            this.btnAddDirector.TabIndex = 109;
            this.btnAddDirector.TabStop = false;
            this.btnAddDirector.Text = "+";
            this.btnAddDirector.UseVisualStyleBackColor = true;
            this.btnAddDirector.Click += new System.EventHandler(this.btnAddDirector_Click);
            // 
            // cmbDirector
            // 
            this.cmbDirector.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDirector.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDirector.FormattingEnabled = true;
            this.cmbDirector.Location = new System.Drawing.Point(50, 330);
            this.cmbDirector.Name = "cmbDirector";
            this.cmbDirector.Size = new System.Drawing.Size(191, 21);
            this.cmbDirector.TabIndex = 107;
            this.cmbDirector.Enter += new System.EventHandler(this.cmbDirector_Enter);
            this.cmbDirector.Leave += new System.EventHandler(this.cmbDirector_Leave);
            // 
            // btnDelCast
            // 
            this.btnDelCast.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDelCast.Location = new System.Drawing.Point(301, 373);
            this.btnDelCast.Name = "btnDelCast";
            this.btnDelCast.Size = new System.Drawing.Size(24, 23);
            this.btnDelCast.TabIndex = 105;
            this.btnDelCast.TabStop = false;
            this.btnDelCast.Text = "-";
            this.btnDelCast.UseVisualStyleBackColor = true;
            this.btnDelCast.Click += new System.EventHandler(this.btnDelCast_Click);
            // 
            // lstCast
            // 
            this.lstCast.FormattingEnabled = true;
            this.lstCast.Location = new System.Drawing.Point(334, 262);
            this.lstCast.Name = "lstCast";
            this.lstCast.Size = new System.Drawing.Size(221, 134);
            this.lstCast.TabIndex = 104;
            this.lstCast.TabStop = false;
            this.lstCast.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstCast_KeyDown);
            // 
            // btnAddCast
            // 
            this.btnAddCast.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAddCast.Location = new System.Drawing.Point(532, 231);
            this.btnAddCast.Name = "btnAddCast";
            this.btnAddCast.Size = new System.Drawing.Size(23, 23);
            this.btnAddCast.TabIndex = 103;
            this.btnAddCast.TabStop = false;
            this.btnAddCast.Text = "+";
            this.btnAddCast.UseVisualStyleBackColor = true;
            this.btnAddCast.Click += new System.EventHandler(this.btnAddCast_Click);
            // 
            // cmbCast
            // 
            this.cmbCast.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCast.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCast.FormattingEnabled = true;
            this.cmbCast.Location = new System.Drawing.Point(334, 233);
            this.cmbCast.Name = "cmbCast";
            this.cmbCast.Size = new System.Drawing.Size(191, 21);
            this.cmbCast.TabIndex = 98;
            this.cmbCast.Enter += new System.EventHandler(this.cmbCast_Enter);
            this.cmbCast.Leave += new System.EventHandler(this.cmbCast_Leave);
            // 
            // lblCast
            // 
            this.lblCast.AutoSize = true;
            this.lblCast.Location = new System.Drawing.Point(331, 212);
            this.lblCast.Name = "lblCast";
            this.lblCast.Size = new System.Drawing.Size(28, 13);
            this.lblCast.TabIndex = 102;
            this.lblCast.Text = "Cast";
            // 
            // btnDelType
            // 
            this.btnDelType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDelType.Location = new System.Drawing.Point(304, 172);
            this.btnDelType.Name = "btnDelType";
            this.btnDelType.Size = new System.Drawing.Size(24, 23);
            this.btnDelType.TabIndex = 101;
            this.btnDelType.TabStop = false;
            this.btnDelType.Text = "-";
            this.btnDelType.UseVisualStyleBackColor = true;
            this.btnDelType.Click += new System.EventHandler(this.btnDelType_Click);
            // 
            // btnAddType
            // 
            this.btnAddType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAddType.Location = new System.Drawing.Point(532, 33);
            this.btnAddType.Name = "btnAddType";
            this.btnAddType.Size = new System.Drawing.Size(23, 23);
            this.btnAddType.TabIndex = 106;
            this.btnAddType.TabStop = false;
            this.btnAddType.Text = "+";
            this.btnAddType.UseVisualStyleBackColor = true;
            this.btnAddType.Click += new System.EventHandler(this.btnAddType_Click);
            // 
            // lstType
            // 
            this.lstType.FormattingEnabled = true;
            this.lstType.Location = new System.Drawing.Point(334, 61);
            this.lstType.Name = "lstType";
            this.lstType.Size = new System.Drawing.Size(221, 134);
            this.lstType.TabIndex = 100;
            this.lstType.TabStop = false;
            // 
            // cmbType
            // 
            this.cmbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(334, 33);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(191, 21);
            this.cmbType.TabIndex = 97;
            this.cmbType.Enter += new System.EventHandler(this.cmbType_Enter);
            this.cmbType.Leave += new System.EventHandler(this.cmbType_Leave);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(331, 9);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 13);
            this.lblType.TabIndex = 99;
            this.lblType.Text = "Type";
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(68, 265);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(100, 20);
            this.txtYear.TabIndex = 96;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(15, 268);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(29, 13);
            this.lblYear.TabIndex = 95;
            this.lblYear.Text = "Year";
            // 
            // chkPicture
            // 
            this.chkPicture.AutoSize = true;
            this.chkPicture.Location = new System.Drawing.Point(30, 178);
            this.chkPicture.Name = "chkPicture";
            this.chkPicture.Size = new System.Drawing.Size(87, 17);
            this.chkPicture.TabIndex = 94;
            this.chkPicture.Text = "Picture is OK";
            this.chkPicture.UseVisualStyleBackColor = true;
            // 
            // picImdb
            // 
            this.picImdb.Location = new System.Drawing.Point(30, 12);
            this.picImdb.Name = "picImdb";
            this.picImdb.Size = new System.Drawing.Size(110, 160);
            this.picImdb.TabIndex = 93;
            this.picImdb.TabStop = false;
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Location = new System.Drawing.Point(12, 233);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(38, 13);
            this.lblRating.TabIndex = 92;
            this.lblRating.Text = "Rating";
            // 
            // txtRating
            // 
            this.txtRating.Location = new System.Drawing.Point(68, 230);
            this.txtRating.Name = "txtRating";
            this.txtRating.Size = new System.Drawing.Size(100, 20);
            this.txtRating.TabIndex = 91;
            // 
            // imdbInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 579);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnSelectNone);
            this.Controls.Add(this.chkLanguage);
            this.Controls.Add(this.chkCast);
            this.Controls.Add(this.chkType);
            this.Controls.Add(this.chkWriter);
            this.Controls.Add(this.chkDirector);
            this.Controls.Add(this.chkYear);
            this.Controls.Add(this.chkRating);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.btnDelLanguage);
            this.Controls.Add(this.lstLanguage);
            this.Controls.Add(this.btnAddLanguage);
            this.Controls.Add(this.cmbLanguage);
            this.Controls.Add(this.lblWriter);
            this.Controls.Add(this.lblDirector);
            this.Controls.Add(this.btnDelWriter);
            this.Controls.Add(this.lstWriter);
            this.Controls.Add(this.btnAddWriter);
            this.Controls.Add(this.cmbWriter);
            this.Controls.Add(this.btnDelDirector);
            this.Controls.Add(this.lstDirector);
            this.Controls.Add(this.btnAddDirector);
            this.Controls.Add(this.cmbDirector);
            this.Controls.Add(this.btnDelCast);
            this.Controls.Add(this.lstCast);
            this.Controls.Add(this.btnAddCast);
            this.Controls.Add(this.cmbCast);
            this.Controls.Add(this.lblCast);
            this.Controls.Add(this.btnDelType);
            this.Controls.Add(this.btnAddType);
            this.Controls.Add(this.lstType);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.chkPicture);
            this.Controls.Add(this.picImdb);
            this.Controls.Add(this.lblRating);
            this.Controls.Add(this.txtRating);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "imdbInfoForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "imdbInfo";
            this.Load += new System.EventHandler(this.imdbInfoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picImdb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.CheckBox chkLanguage;
        private System.Windows.Forms.CheckBox chkCast;
        private System.Windows.Forms.CheckBox chkType;
        private System.Windows.Forms.CheckBox chkWriter;
        private System.Windows.Forms.CheckBox chkDirector;
        private System.Windows.Forms.CheckBox chkYear;
        private System.Windows.Forms.CheckBox chkRating;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Button btnDelLanguage;
        private System.Windows.Forms.ListBox lstLanguage;
        private System.Windows.Forms.Button btnAddLanguage;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Label lblWriter;
        private System.Windows.Forms.Label lblDirector;
        private System.Windows.Forms.Button btnDelWriter;
        private System.Windows.Forms.ListBox lstWriter;
        private System.Windows.Forms.Button btnAddWriter;
        private System.Windows.Forms.ComboBox cmbWriter;
        private System.Windows.Forms.Button btnDelDirector;
        private System.Windows.Forms.ListBox lstDirector;
        private System.Windows.Forms.Button btnAddDirector;
        private System.Windows.Forms.ComboBox cmbDirector;
        private System.Windows.Forms.Button btnDelCast;
        private System.Windows.Forms.ListBox lstCast;
        private System.Windows.Forms.Button btnAddCast;
        private System.Windows.Forms.ComboBox cmbCast;
        private System.Windows.Forms.Label lblCast;
        private System.Windows.Forms.Button btnDelType;
        private System.Windows.Forms.Button btnAddType;
        private System.Windows.Forms.ListBox lstType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.CheckBox chkPicture;
        private System.Windows.Forms.PictureBox picImdb;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.TextBox txtRating;
    }
}