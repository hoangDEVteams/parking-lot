using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test.Views
{
    public partial class FResetPass : Form
    {
        private string username;
        public FResetPass(string username)
        {
            InitializeComponent();
            this.username = username;
            txtPassReset.PasswordChar = '*'; 
            txtCFPass2.PasswordChar = '*';
        }

        private void FResetPass_Load(object sender, EventArgs e)
        {
            label1.Text = "Hello, " + username;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string newPassword = txtPassReset.Text.Trim();
            string confirmPassword = txtCFPass2.Text.Trim();
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu mới và xác nhận mật khẩu.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp.");
                return;
            }
            var account = CUltils.db.Accounts.SingleOrDefault(a => a.Username == username);
            if (account == null)
            {
                MessageBox.Show("Tài khoản không tồn tại.");
                return;
            }
            string salt = account.Salt;

            string hashedPassword = CPass.HashPasswordWithSalt(newPassword, salt);
            account.Password = hashedPassword;
            CUltils.db.SaveChanges();
            MessageBox.Show("Đặt lại mật khẩu thành công!");
            this.Close();
            FLogin fLogin = new FLogin();
            fLogin.Show();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassReset.PasswordChar = '\0'; 
                txtCFPass2.PasswordChar = '\0';   
            }
            else
            {
                txtPassReset.PasswordChar = '*';   
                txtCFPass2.PasswordChar = '*';    
            }

        }
    }
}
