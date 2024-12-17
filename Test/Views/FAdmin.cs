using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Test.Controller;
using System.Net;

namespace Test.Views
{
    public partial class FAdmin : Form
    {
        public FAdmin()
        {
            InitializeComponent();
        }
        private void LoadAccount()
        {
            Ctrl_Account ctrlAccount = new Ctrl_Account();
            var account = Ctrl_Account.GetAccounts();
            dataGridView1.DataSource = account;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            comboBox1.Items.Clear();  
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                comboBox1.Items.Add(column.HeaderText); 
            }

        }

        private void FAdmin_Load(object sender, EventArgs e)
        {
            cmbChangeRole.Visible = false;
            LoadAccount();
            cmbChangeRole.Items.AddRange(new string[] { "Customer", "Employee", "Admin" });
            cmbChangeRole.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            txtSearch.TextChanged += textBox1_TextChanged;

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) 
            {
                cmbChangeRole.Visible = true; 
                cmbChangeRole.BringToFront(); 
                cmbChangeRole.Location = new Point(btnChangeRole.Location.X, btnChangeRole.Location.Y + btnChangeRole.Height);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tài khoản trước.");
            }
        }
        private void UpdateUserRole(int idAcc, string role)
        {
            Ctrl_Account.ChangeRole(idAcc, role);
            LoadAccount();
        }
        private void cmbChangeRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string selectedRole = cmbChangeRole.SelectedItem.ToString();
                int selectedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IDAcc"].Value);
                UpdateUserRole(selectedID, selectedRole);
                if(selectedRole == "Employee")
                {
                    string position = Microsoft.VisualBasic.Interaction.InputBox(
                    "Vui lòng nhập vị trí công việc:", 
                    "Nhập vị trí",                   
                    "");                            

                    if (!string.IsNullOrEmpty(position)) 
                    {
                        Ctrl_User.UpdateRoleUserEm(selectedID, position); 
                    }
                    else
                    {
                        MessageBox.Show("Vị trí không được để trống. Vui lòng thử lại.");
                        return;
                    }
                }
                else if(selectedRole == "Admin")
                {
                    Ctrl_User.UpdateRoleUserAD(selectedID);
                }
                else
                {
                    Ctrl_User.UpdateRoleUserCus(selectedID);

                }
                MessageBox.Show($"Đã đổi role cho tài khoản {selectedID} thành {selectedRole}.");
                cmbChangeRole.Visible = false; 
            }
        }

        private void iconButton3_Click_1(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IDAcc"].Value);
                string selectedRole = dataGridView1.SelectedRows[0].Cells["Role"].Value.ToString();
                string UserID = dataGridView1.SelectedRows[0].Cells["IDUser"].Value.ToString();
                if (selectedRole != "Employee")
                {
                    MessageBox.Show("This account is not an Employee!");
                    return;
                }
                else
                {
                    decimal salary = Convert.ToDecimal(Microsoft.VisualBasic.Interaction.InputBox("Vui lòng nhập mức lương cho nhân viên:",
                    "Nhập mức lương",                  
                    ""));
                    Ctrl_User.UpdateSalaryForEmp(salary, UserID);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                FilterDataGridView();
            }
        }
        private void FilterDataGridView()
        {
            string selectedColumn = comboBox1.SelectedItem.ToString();
            string searchValue = txtSearch.Text.ToLower();

            CurrencyManager currencyManager = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            currencyManager.SuspendBinding();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[selectedColumn].Value != null)
                {
                    string cellValue = row.Cells[selectedColumn].Value.ToString().ToLower();
                    row.Visible = cellValue.Contains(searchValue);
                }
            }

            currencyManager.ResumeBinding();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                FilterDataGridView();
            }
            else
            {
                LoadAccount();
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string chartFilePath = Ctrl_Chart.GenerateBarChart();

            // Gửi email với biểu đồ đính kèm
            Ctrl_Account.SendEmailWithChart("hoanghuanpham3@gmail.com", chartFilePath);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            string chartFilePath = Ctrl_Chart.GenerateAndSendRevenueChart();

            if (!string.IsNullOrEmpty(chartFilePath))
            {
                Ctrl_Account.SendEmailWithAttachment("hoanghuanpham3@gmail.com", chartFilePath);
                MessageBox.Show("Email đã được gửi thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không thể tạo biểu đồ. Vui lòng kiểm tra dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Ctrl_Chart chartController = new Ctrl_Chart();
            string recipientEmail = "hoanghuanpham3@gmail.com"; // Địa chỉ email nhận
           
            Ctrl_Chart.GenerateAndSendUserStatisticsChart(recipientEmail);
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            Ctrl_Chart chartController = new Ctrl_Chart();
            string recipientEmail = "hoanghuanpham3@gmail.com"; // Địa chỉ email nhận

            // Tạo và gửi biểu đồ
            chartController.GenerateAndSendUserComparisonChart(recipientEmail);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
