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
    public partial class FFillRental : Form
    {
        Ctrl_VehicleTypes ctrlVehicleType = new Ctrl_VehicleTypes();
        public int IDVehicle;
        public FFillRental(string text)
        {
            InitializeComponent();
            IDVehicle = Int32.Parse(text);
        }
        private void FFillRental_Load(object sender, EventArgs e)
        {
            VehicleType vehicle = ctrlVehicleType.GetVehiclesbyCategory(IDVehicle);
            txtID.Text = IDVehicle.ToString();
            txtName.Text = vehicle.VehicleTypeName.ToString();
            txtFacturer.Text = vehicle.Manufacturer.ToString();
        }

        private void lblTotalPrice_Click(object sender, EventArgs e)
        {

        }
    }
}
