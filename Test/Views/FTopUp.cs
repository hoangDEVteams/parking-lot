﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private void FTopUp_Load(object sender, EventArgs e)
        {

        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            amount = 50000;
            string result = await MomoPayment.MomoPayment.CreatePaymentRequest(amount);

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

        private async Task HandleIconButton1ClickAsync(object sender, EventArgs e)
        {
            
        }


        private async void iconButton2_Click(object sender, EventArgs e)
        {
            amount = 100000;
            string result = await MomoPayment.MomoPayment.CreatePaymentRequest(amount);
            
            // Hiển thị kết quả
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
                        UseShellExecute = true // Đảm bảo mở trình duyệt mặc định
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

        private void iconButton3_Click(object sender, EventArgs e)
        {
            amount = 2000000;
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            amount = 5000000;

        }

    }
}
