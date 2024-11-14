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
        public FVerify(string username)
        {
            InitializeComponent();

            this.username = username;
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

                        MessageBox.Show(result);

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
    }
}