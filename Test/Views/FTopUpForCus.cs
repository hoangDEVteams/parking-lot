using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Test.Controller;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Test.Views
{
    public partial class FTopUpForCus : Form
    {
        private int IDAcc = 0;
        private decimal amount;
        public FTopUpForCus()
        {
            InitializeComponent();
        }
        private void LoadInfo()
        {
            Ctrl_Account ctrlAccount = new Ctrl_Account();
            Ctrl_User ctrl_User = new Ctrl_User();
            Ctrl_Wallet ctrl_Wallet = new Ctrl_Wallet();

            if (IDAcc >= 0)
            {
                var user = ctrl_User.GetUserByIDAcc(IDAcc);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.DataSource = user;
                groupBox3.Visible = true;

            }
        }
        private void FTopUpForCus_Load(object sender, EventArgs e)
        {
            LoadInfo();
            groupBox3.Visible = false;
            dataGridView2.MouseEnter += dataGridView2_MouseEnter;
            dataGridView2.MouseLeave += dataGridView2_MouseLeave;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int result))
            {
                IDAcc = result;
                label6.Text = IDAcc+"";
                LoadInfo();
                
            }
            else
            {
                MessageBox.Show("Vui lòng nhập một số hợp lệ!");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private string ShowInputDialog(string text)
        {
            System.Windows.Forms.Form prompt = new System.Windows.Forms.Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog,
                Text = text,
                StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            };

            System.Windows.Forms.Label textLabel = new System.Windows.Forms.Label() { Left = 10, Top = 10, Text = text };
            System.Windows.Forms.TextBox inputTextBox = new System.Windows.Forms.TextBox() { Left = 10, Top = 50, Width = 260 };
            System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button() { Text = "OK", Left = 190, Width = 80, Top = 80, DialogResult = System.Windows.Forms.DialogResult.OK };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputTextBox);
            prompt.Controls.Add(confirmation);

            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == System.Windows.Forms.DialogResult.OK ? inputTextBox.Text : null;
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            amount = 5000;
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

        private async void button6_Click(object sender, EventArgs e)
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
        private void dataGridView2_MouseEnter(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView2_MouseLeave(object sender, EventArgs e)
        {
            Hide();
        }
        private void Hide()
        {

            dataGridView2.DataSource = null;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void LoadData()
        {
            var Acc = Ctrl_Account.GetAccounts();
            dataGridView2.DataSource = Acc;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {
            

        }
        private void groupBox2_Leave(object sender, EventArgs e)
        {

        }
        private void dataLeave()
        {
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataLeave(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
