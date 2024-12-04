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
using Test.MomoPayment;

namespace Test.Views
{
    public partial class FTopUp : Form
    {
        private string username;
        private decimal amount;



        public FTopUp(string username)
        {
            InitializeComponent();
            this.username = username;
            this.amount = amount;

        }

        private async void FTopUp_Load(object sender, EventArgs e)
        {

        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            amount = 50000;
            int IDAcc = Ctrl_Account.GetIDAcc(username);
            string result = await MomoPayment.MomoPayment.CreatePaymentRequest(amount, IDAcc);

            try
            {
                var json = Newtonsoft.Json.Linq.JObject.Parse(result);
                string payUrl = json["payUrl"]?.ToString();

                if (!string.IsNullOrEmpty(payUrl))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = payUrl,
                        UseShellExecute = true 
                    });
                }
                else
                {
                    MessageBox.Show("Không tìm thấy payUrl trong kết quả.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Error");
            }
        }

        private async Task HandleIconButton1ClickAsync(object sender, EventArgs e)
        {
            
        }


        private async void iconButton2_Click(object sender, EventArgs e)
        {
            amount = 100000;
            int IDAcc = Ctrl_Account.GetIDAcc(username);
            string result = await MomoPayment.MomoPayment.CreatePaymentRequest(amount, IDAcc);

            try
            {
                var json = Newtonsoft.Json.Linq.JObject.Parse(result);
                string payUrl = json["payUrl"]?.ToString();

                if (!string.IsNullOrEmpty(payUrl))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = payUrl,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Không tìm thấy payUrl trong kết quả.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Error");
            }

        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            amount = 2000000;
            int IDAcc = Ctrl_Account.GetIDAcc(username);
            string result = await MomoPayment.MomoPayment.CreatePaymentRequest(amount, IDAcc);

            try
            {
                var json = Newtonsoft.Json.Linq.JObject.Parse(result);
                string payUrl = json["payUrl"]?.ToString();

                if (!string.IsNullOrEmpty(payUrl))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = payUrl,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Không tìm thấy payUrl trong kết quả.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Error");
            }

        }

        private async void iconButton4_Click(object sender, EventArgs e)
        {
            amount = 5000000;
            int IDAcc = Ctrl_Account.GetIDAcc(username);
            string result = await MomoPayment.MomoPayment.CreatePaymentRequest(amount, IDAcc);

            MessageBox.Show(result, "Payment Result");
            try
            {
                var json = Newtonsoft.Json.Linq.JObject.Parse(result);
                string payUrl = json["payUrl"]?.ToString();

                if (!string.IsNullOrEmpty(payUrl))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = payUrl,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Không tìm thấy payUrl trong kết quả.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Error");
            }

        }
        private string ShowInputDialog(string text)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = text,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 10, Top = 20, Text = text };
            TextBox inputBox = new TextBox() { Left = 10, Top = 50, Width = 260 };
            Button confirmation = new Button() { Text = "OK", Left = 190, Width = 80, Top = 80, DialogResult = DialogResult.OK };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : null;
        }

        private async void iconButton6_Click(object sender, EventArgs e)
        {
            string input = ShowInputDialog("Nhập số tiền (bội số của 50,000):");

            if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int amount) || amount <= 0)
            {
                MessageBox.Show("Vui lòng nhập một số tiền hợp lệ.", "Thông báo");
                return;
            }

            if (amount % 50000 != 0)
            {
                MessageBox.Show("Số tiền phải là bội số của 50,000.", "Thông báo");
                return;
            }

            int IDAcc = Ctrl_Account.GetIDAcc(username);
            string result = await MomoPayment.MomoPayment.CreatePaymentRequest(amount, IDAcc);

            MessageBox.Show(result, "Payment Result");
            try
            {
                var json = Newtonsoft.Json.Linq.JObject.Parse(result);
                string payUrl = json["payUrl"]?.ToString();

                if (!string.IsNullOrEmpty(payUrl))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = payUrl,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Không tìm thấy payUrl trong kết quả.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Error");
            }
        }

        private async void iconButton5_Click(object sender, EventArgs e)
        {
            amount = 10000000;
            int IDAcc = Ctrl_Account.GetIDAcc(username);
            string result = await MomoPayment.MomoPayment.CreatePaymentRequest(amount, IDAcc);

            MessageBox.Show(result, "Payment Result");
            try
            {
                var json = Newtonsoft.Json.Linq.JObject.Parse(result);
                string payUrl = json["payUrl"]?.ToString();

                if (!string.IsNullOrEmpty(payUrl))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = payUrl,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Không tìm thấy payUrl trong kết quả.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Error");
            }
        }
    }
}
