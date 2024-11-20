using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Controller;

namespace Test.Views
{

    public partial class FForgotPassword : Form
    {
        private string selectedUsername;
        public FForgotPassword()
        {
            InitializeComponent();
            label1.Visible = false; 
            textBox1.Visible = false;
            button1.Visible = false;
            label2.Visible = false;
            comboBox1.Visible = false;
            button2.MouseEnter += button2_MouseEnter;
            button2.MouseLeave += button2_MouseLeave;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string input = txtEmailOrUser.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Vui lòng nhập Email hoặc Tên đăng nhập.");
                return;
            }

            if (input.Contains("@"))
            {
                // Input is an email
                var accounts = CUltils.db.Accounts.Where(a => a.Email == input).ToList();
                if (accounts.Any())
                {
                    comboBox1.Items.Clear();
                    foreach (var account in accounts)
                    {
                        comboBox1.Items.Add(account.Username);
                    }
                    comboBox1.Visible = true;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản với email này.");
                }
            }
            else
            {
                // Input is a username
                var account = CUltils.db.Accounts.SingleOrDefault(a => a.Username == input);
                if (account != null)
                {
                    string verificationCode = Ctrl_Account.GenerateVerificationCode();
                    Ctrl_Account.SendVerificationEmail(account.Email, verificationCode);
                    Ctrl_Account.SaveVerificationCodeToDatabase(account.Username, verificationCode);
                    label1.Visible = true;
                    textBox1.Visible = true;
                    button1.Visible = true;
                    label2.Visible = true;
                    MessageBox.Show("Mã xác nhận đã được gửi đến email của bạn.");
                }
                else
                {
                    MessageBox.Show("Tài khoản không tồn tại. Vui lòng kiểm tra lại thông tin.");
                }
            }
        }

        private void txtEmailOrUser_TextChanged(object sender, EventArgs e)
        {
            string input = txtEmailOrUser.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                comboBox1.Items.Clear();
                comboBox1.Visible = false;
                return;
            }

            if (input.Contains("@"))
            {
                // Input is an email
                var accounts = CUltils.db.Accounts.Where(a => a.Email == input).ToList();
                if (accounts.Any())
                {
                    comboBox1.Items.Clear();
                    foreach (var account in accounts)
                    {
                        comboBox1.Items.Add(account.Username);
                    }
                    comboBox1.Visible = true;
                }
                else
                {
                    comboBox1.Visible = false;
                }
            }
            else
            {
                comboBox1.Visible = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredCode = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(enteredCode))
            {
                MessageBox.Show("Vui lòng nhập mã xác nhận.");
                return;
            }
            var account = CUltils.db.Accounts.SingleOrDefault(a => a.Username == selectedUsername);

            if (account != null && account.VerificationCode == enteredCode)
            {
                this.Close();
                FResetPass resetForm = new FResetPass(account.Username);
                resetForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Mã xác nhận không chính xác. Vui lòng thử lại.");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                selectedUsername = comboBox1.SelectedItem.ToString(); 
            }
        }

        private void FForgotPassword_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            FLogin fLogin = new FLogin();
            fLogin.Show();
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
