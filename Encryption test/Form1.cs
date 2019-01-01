using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Encryption_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

         byte[] encrypted;
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            MD5CryptoServiceProvider md = new MD5CryptoServiceProvider();
            UTF8Encoding ut = new UTF8Encoding();
            TripleDESCryptoServiceProvider tds = new TripleDESCryptoServiceProvider();
            tds.Key = md.ComputeHash(ut.GetBytes(txtKey.Text));
            tds.Mode = CipherMode.ECB;
            tds.Padding = PaddingMode.PKCS7;
            ICryptoTransform trans = tds.CreateEncryptor();
            encrypted = trans.TransformFinalBlock(ut.GetBytes(txtToEncrypt.Text), 0,
                ut.GetBytes(txtToEncrypt.Text).Length);
            txtEncrypted.Text = BitConverter.ToString(encrypted);
        }

        private void btnDecrypted_Click(object sender, EventArgs e)
        {
            MD5CryptoServiceProvider md = new MD5CryptoServiceProvider();
            UTF8Encoding ut = new UTF8Encoding();
            TripleDESCryptoServiceProvider tds = new TripleDESCryptoServiceProvider();
            tds.Key = md.ComputeHash(ut.GetBytes(txtDecryptKey.Text));
            tds.Mode = CipherMode.ECB;
            tds.Padding = PaddingMode.PKCS7;
            ICryptoTransform trans = tds.CreateDecryptor();
            txtDecrypted.Text = ut.GetString(trans.TransformFinalBlock(encrypted, 0, encrypted.Length));
        }
    }
}
