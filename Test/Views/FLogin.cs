using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Test.Views
{
    public partial class FLogin : Form
    {
        public FLogin()
        {
            InitializeComponent();
            button2.MouseEnter += button2_MouseEnter;
            button2.MouseLeave += button2_MouseLeave;
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
            if (checkBox2.Checked)
            {
                Properties.Settings.Default.Username = username;
                Properties.Settings.Default.Password = password; // Lưu mật khẩu đã mã hóa
                Properties.Settings.Default.RememberMe = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.Save();
            }
            FMain fMain = new FMain(username);
            fMain.Show();
            this.Hide();

        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            txtPassLog.PasswordChar = '*';
            if (Properties.Settings.Default.RememberMe)
            {
                txtUserLog.Text = Properties.Settings.Default.Username;
                txtPassLog.Text = Properties.Settings.Default.Password;

                checkBox2.Checked = true;
            }
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

        private void label1_Click(object sender, EventArgs e)
        {
            FForgotPassword f = new FForgotPassword();
            f.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.Red;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Transparent;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Enter(object sender, EventArgs e)
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
            if (checkBox2.Checked)
            {
                Properties.Settings.Default.Username = username;
                Properties.Settings.Default.Password = password; 
                Properties.Settings.Default.RememberMe = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.Save();
            }
            FMain fMain = new FMain(username);
            fMain.Show();
            this.Hide();

        }

        private void FLogin_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtPassLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassLog_Enter(object sender, EventArgs e)
        {

        }

        private void txtUserLog_Enter(object sender, EventArgs e)
        {

        }
    }
}
