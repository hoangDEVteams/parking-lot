using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Controller;
using Test.Views;

namespace Test
{
    public partial class FRegister : Form
    {
        public FRegister()
        {
            InitializeComponent();
            button2.MouseEnter += button2_MouseEnter;
            button2.MouseLeave += button2_MouseLeave;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtPass.PasswordChar = '*';
            txtCFPass.PasswordChar = '*';
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text;
            string email = txtEmail.Text;
            string password = txtPass.Text;
            string confirmPassword = txtCFPass.Text;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp.");
                return;
            }

            try
            {
                var mail = new System.Net.Mail.MailAddress(email.Trim()); 
            }
            catch
            {
                MessageBox.Show("Email không hợp lệ.");
                return;
            }

            var existingUser = CUltils.db.Accounts.SingleOrDefault(a => a.Username == username);
            if (existingUser != null)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại.");
                return;
            }

            string salt = CPass.GenerateSalt();

            string hashedPassword = CPass.HashPasswordWithSalt(password, salt);
            string result = Ctrl_Account.RegisterAccount(username, hashedPassword, email, salt);

            if (result == "Đăng ký thành công!")
            {
                string verificationCode = Ctrl_Account.GenerateVerificationCode();
                Ctrl_Account.SendVerificationEmail(email, verificationCode);
                Ctrl_Account.SaveVerificationCodeToDatabase(username, verificationCode);
                var verifyForm = new FVerify(username);
                verifyForm.ShowDialog();

            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            FLogin fLogin = new FLogin();
            fLogin.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtUser.Text = "";
            txtEmail.Text = "";
            txtPass.Text = "";
            txtCFPass.Text = "";
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

    }
}
        
