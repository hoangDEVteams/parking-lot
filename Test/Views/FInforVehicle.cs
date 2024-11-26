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
    public partial class FInforVehicle : Form
    {
        CTrl_Vehicles ctrlVehicle = new CTrl_Vehicles();
        Ctrl_VehicleTypes ctrlVehicleType = new Ctrl_VehicleTypes();
        private int IDvehicle;
        
        public FInforVehicle(int idvehicle)
        {
            InitializeComponent();
            IDvehicle = idvehicle;
            showinfor();
        }
        public void showinfor()
        {
            var vehicleType = ctrlVehicleType.GetVehiclesbyCategory(IDvehicle);
            var vehicle = ctrlVehicle.GetVehicleByID(IDvehicle);
            if (vehicleType != null && vehicle != null)
            {
                lblInforVehicle.Text = $"{vehicleType.VehicleTypeName} {vehicleType.ManufactureYear}-{vehicle.LicensePlate} - {vehicle.Color}";
            }
            else
            {
                lblInforVehicle.Text = "Không tìm thấy thông tin xe.";
            }
        }

        private void FInforVehicle_Load(object sender, EventArgs e)
        {
            var vehicleType = ctrlVehicleType.GetVehiclesbyCategory(IDvehicle);
            var vehicle = ctrlVehicle.GetVehicleByID(IDvehicle);
            lblName.Text = vehicleType.VehicleTypeName;
            lblLicensePlate.Text = vehicle.LicensePlate;
            lblColor.Text = vehicle.Color;
            lblStatus.Text = vehicle.Status;
            lblDes.Text = vehicleType.Description;
        }

        private void lblExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
