namespace TestWinForm {
	partial class bulkHop {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose ( bool disposing ) {
			if ( disposing && ( components != null ) ) {
				components.Dispose ( );
			}
			base.Dispose ( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ( ) {
			this.btnSave = new System.Windows.Forms.Button();
			this.txtId = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.txtResExt = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(12, 678);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(514, 60);
			this.btnSave.TabIndex = 0;
			this.btnSave.Text = "button1";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// txtId
			// 
			this.txtId.Location = new System.Drawing.Point(12, 51);
			this.txtId.Multiline = true;
			this.txtId.Name = "txtId";
			this.txtId.Size = new System.Drawing.Size(79, 621);
			this.txtId.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(39, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(18, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "ID";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(200, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Path";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(414, 19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Res && Ext";
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(97, 51);
			this.txtPath.Multiline = true;
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(261, 621);
			this.txtPath.TabIndex = 5;
			// 
			// txtResExt
			// 
			this.txtResExt.Location = new System.Drawing.Point(364, 51);
			this.txtResExt.Multiline = true;
			this.txtResExt.Name = "txtResExt";
			this.txtResExt.Size = new System.Drawing.Size(162, 621);
			this.txtResExt.TabIndex = 6;
			// 
			// bulkHop
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(542, 750);
			this.Controls.Add(this.txtResExt);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtId);
			this.Controls.Add(this.btnSave);
			this.Name = "bulkHop";
			this.Text = "Bulk Hop Hoop";
			this.Load += new System.EventHandler(this.bulkHop_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox txtId;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.TextBox txtResExt;
	}
}

