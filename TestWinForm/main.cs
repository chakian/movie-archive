using com.cagdaskorkut.utility.Security;
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
            txtOutput.Text = Encryption.Encrypt(txtInput.Text);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            txtOutput.Text = Encryption.Decrypt(txtInput.Text);
        }
    }
}
