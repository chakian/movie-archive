using MArchiveLibrary.Helpers;
using System;
using System.Windows.Forms;

namespace TestWinForm
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            txtOutput.Text = EncryptionHelper.Encrypt(txtInput.Text);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            txtOutput.Text = EncryptionHelper.Decrypt(txtInput.Text);
        }
    }
}
