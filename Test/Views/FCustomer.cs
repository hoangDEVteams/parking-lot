using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using Test.Controller;

namespace Test.Views
{
    public partial class FCustomer : Form
    {
        Ctrl_Customers ctrCustomers = new Ctrl_Customers(); // Controller xử lý dữ liệu khách hàng

        public FCustomer()
        {
            InitializeComponent();
        }

       
        private void FCustomer_Load(object sender, EventArgs e)
        {

            
                var customers = CUltils.db.Users.ToList();
                dtgridDSKH.DataSource = customers; // Gán dữ liệu vào DataGridView
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCMND_TextChanged(object sender, EventArgs e)
        {
            if (txtCMND.Text.Length > 12)
            {
                txtCMND.Text = txtCMND.Text.Substring(0, 12);
                txtCMND.SelectionStart = txtCMND.Text.Length;
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dtgridDSKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CUltils.db.Customers.Add(new Customer());
            CUltils.db.SaveChanges();
        }

        
    }
}