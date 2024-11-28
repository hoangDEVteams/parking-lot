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
    public partial class FRentals : Form
    {
        public FRentals()
        {
            InitializeComponent();
        }
        private void LoadUserData()
        {
            Ctrl_User user = new Ctrl_User();
            var userData = user.UserData();
            Ctrl_Rental rental = new Ctrl_Rental();
            var rentalData = rental.RentalData();


            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.DataSource = rentalData;
            dataGridView2.ScrollBars = ScrollBars.Horizontal;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = userData;
            dataGridView1.ScrollBars = ScrollBars.Horizontal;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FRentals_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }
    }
}
