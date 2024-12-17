using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Controller;

namespace Test.Views
{
    public partial class FReport : Form
    {
        private ReportChart _reportChart;
        private string imagePath = string.Empty;
        public FReport()
        {
            InitializeComponent();
            _reportChart = new ReportChart();  
             
            startDatePicker.Value = DateTime.Now.AddMonths(-1); 
            endDatePicker.Value = DateTime.Now; 
            reportTypeComboBox.Items.Add("Customer Rentals");
            reportTypeComboBox.Items.Add("Penalty Amounts");
            reportTypeComboBox.Items.Add("Total Earnings");
            reportTypeComboBox.Items.Add("Employee Salaries");
            reportTypeComboBox.SelectedIndex = 0;
        }

        private void FReport_Load(object sender, EventArgs e)
        {
            Ctrl_Account ctrlAccount = new Ctrl_Account();
            var account = Ctrl_Account.GetADAccount();
            dataGridView1.DataSource = account;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = startDatePicker.Value;
            DateTime endDate = endDatePicker.Value;
            string reportType = reportTypeComboBox.SelectedItem.ToString();
             
            string projectDirectory = Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName;
            string imgDirectory = Path.Combine(projectDirectory, "Resource", "img");
             
            if (!Directory.Exists(imgDirectory))
            {
                Directory.CreateDirectory(imgDirectory);
            }
             
            imagePath = Path.Combine(imgDirectory, $"chart_{DateTime.Now:yyyyMMdd_HHmmss}.png");
             
            _reportChart.GenerateColumnChart(reportChart, startDate, endDate, reportType);
             
            ReportChart.SaveChartAsImage(reportChart, imagePath);
        }

        private void reportTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns.Count > 0)
            { 
                string email = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();  
                 
                textBox1.Text = email;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                MessageBox.Show("Vui lòng tạo biểu đồ trước khi gửi email.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string recipientEmail = textBox1.Text;  
            string senderEmail = "hhbakery5@gmail.com";   
            string senderPassword = "vscw ldrh vdfk xgml";   
            string subject = "Báo cáo";
            string body = "Chào bạn, đây là báo cáo của ngày hôm nay.";
             
            Ctrl_Account.SendReportEmailWithImage(recipientEmail, imagePath, subject, body, senderEmail, senderPassword);
        }
    }
}
