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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Test.Views
{
    public partial class FVerify : Form
    {
        private string username;
        private DateTime lastResendTime;
        private Timer resendTimer;
        private int remainingSeconds;
        


        public FVerify(string username)
        {
            InitializeComponent();

            this.username = username;
            resendTimer = new Timer();
            resendTimer.Interval = 1000;
            resendTimer.Tick += ResendTimer_Tick;
            lblTimer.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredCode = txtVerificationCode.Text;
            var account = CUltils.db.Accounts.SingleOrDefault(a => a.Username == username);

            if (account != null)
            {
                if (enteredCode == account.VerificationCode)
                {
                    if (account.VerificationCodeExpiration > DateTime.Now)
                    {
                        string result = Ctrl_Account.UpdateAccountStatusToActive(username);

                        if (result == "Cập nhật trạng thái thành công!")
                        {
                            this.Close(); 
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã xác nhận đã hết hạn. Vui lòng yêu cầu mã xác nhận mới.");
                    }
                }
                else
                {
                    MessageBox.Show("Mã xác nhận không chính xác. Vui lòng thử lại.");
                }
            }
            else
            {
                MessageBox.Show("Tài khoản không tồn tại.");
            }
        }

        private void FVerify_Load(object sender, EventArgs e)
        {

        }

        private void txtResend_Click(object sender, EventArgs e)
        {
            if ((DateTime.Now - lastResendTime).TotalSeconds < 90)
            {
                MessageBox.Show("Bạn chỉ có thể yêu cầu mã xác nhận mới sau 90 giây.");
                return;
            }

            var account = CUltils.db.Accounts.SingleOrDefault(a => a.Username == username);
            if (account != null)
            {
                string newVerificationCode = Ctrl_Account.GenerateVerificationCode();
                account.VerificationCode = newVerificationCode;
                account.VerificationCodeExpiration = DateTime.Now.AddMinutes(10);
                CUltils.db.SaveChanges();

                Ctrl_Account.SendVerificationEmail(account.Email, newVerificationCode);
                MessageBox.Show("Mã xác nhận mới đã được gửi đến email của bạn.");

                lastResendTime = DateTime.Now;
                txtResend.Enabled = false;
                remainingSeconds = 90;
                lblTimer.Visible = true;
                lblTimer.Text = $"{remainingSeconds}";
                resendTimer.Start();
            }
            else
            {
                MessageBox.Show("Tài khoản không tồn tại.");
            }
        }
        private void ResendTimer_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;
            if (remainingSeconds <= 0)
            {
                txtResend.Enabled = true;
                lblTimer.Visible = false;
                resendTimer.Stop();
            }
            else
            {
                lblTimer.Text = $"{remainingSeconds}";
            }
        }

        private void lblTimer_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}