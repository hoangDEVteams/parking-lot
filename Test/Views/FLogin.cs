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
    public partial class FLogin : Form
    {
        public FLogin()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string username = txtUserLog.Text;
            string password = txtPassLog.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin đăng nhập.");
                return;
            }

            var account = (from a in CUltils.db.Accounts
                           where a.Username == username
                           select a).FirstOrDefault();

            if (account == null)
            {
                MessageBox.Show("Tên đăng nhập không tồn tại.");
                return;
            }

            string hashedPassword = CPass.HashPasswordWithSalt(password, account.Salt);
            if (hashedPassword != account.Password)
            {
                MessageBox.Show("Mật khẩu không chính xác.");
                return;
            }
            if (account.Status != "Active")
            {
                MessageBox.Show("Tài khoản của bạn chưa được kích hoạt.");
                return;
            }
            MessageBox.Show("Đăng nhập thành công!");
        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            txtPassLog.PasswordChar = '*';
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                txtPassLog.PasswordChar = '\0';
            }
            else
            {
                txtPassLog.PasswordChar = '*';
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            FRegister f = new FRegister();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtPassLog.Text = "";
            txtUserLog.Text = "";
        }
    }
}
