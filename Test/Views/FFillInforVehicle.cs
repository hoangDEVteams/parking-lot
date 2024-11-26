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
    public partial class FFillInforVehicle : Form
    {
        CTrl_Vehicles ctrlVehicles = new CTrl_Vehicles();
        Ctrl_VehicleTypes ctrlVehicleType = new Ctrl_VehicleTypes();
        private int? IDvehicle;
        public FFillInforVehicle(int? idvehicle = null)
        {
            InitializeComponent();
            IDvehicle = idvehicle;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (IDvehicle.HasValue)
            {
                int id = IDvehicle.Value;
                ctrlVehicleType.GetVehiclesbyCategory(id);
            }
            else
            {
                MessageBox.Show("Vehicle ID is not provided.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text + "";
            string Manufacturer = txtManu.Text + "";
            int Year = DTYear.Value.Year;
            string Description = txtDes.Text + "";

            string LicensePlate = txtPlate.Text + "";
            string Color = cbColor.Text + "";
            string Status = cbStatus.Text + "";
            var newVehicle = new Vehicle
            {
                LicensePlate = LicensePlate,
                Color = Color,
                Status = Status,
                Description = Description
            };
            var newVehicleType = new VehicleType
            {
                VehicleTypeName = name,
                Manufacturer = Manufacturer,
                ManufactureYear = Year,
                Description = Description
            };
            ctrlVehicles.add(newVehicle);
            ctrlVehicleType.AddVehicleType(newVehicleType);
        }
    }
}
