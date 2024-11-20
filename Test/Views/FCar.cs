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
    public partial class FCar : Form
    {
        Ctrl_Vehivles ctrVehicles = new Ctrl_Vehivles();
        Ctrl_VehiclesType ctrvehicleType = new Ctrl_VehiclesType();
        public FCar()
        {
            InitializeComponent();
        }

        private void FCar_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLbaixeDataSet3.Vehicles' table. You can move, or remove it, as needed.
            this.vehiclesTableAdapter1.Fill(this.qLbaixeDataSet3.Vehicles);

        }
        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.vehiclesTableAdapter.FillBy(this.qLbaixeDataSet.Vehicles);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
        #region
        private void CBStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVehicles();
        }
        private void CBColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVehicles();
        }
        private void UpdateVehicleList(List<Vehicle> vehicles)
        {
            dtgridVehicles.DataSource = null; 
            dtgridVehicles.DataSource = vehicles;
        }

        private void FilterVehicles()
        {
            string selectedStatus = CBStatus.SelectedItem?.ToString();
            string selectedColor = CBColor.SelectedItem?.ToString();

            List<Vehicle> filteredVehicles = CUltils.db.Vehicles.ToList();

            if (!string.IsNullOrEmpty(selectedStatus))
            {
                filteredVehicles = filteredVehicles.Where(t => t.Status.Contains(selectedStatus)).ToList();
            }

            if (!string.IsNullOrEmpty(selectedColor))
            {
                filteredVehicles = filteredVehicles.Where(t => t.Color.Contains(selectedColor)).ToList();
            }

            UpdateVehicleList(filteredVehicles);
        }

        #endregion

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            string s = txtFind.Text;
            List<Vehicle> findVehicles = !string.IsNullOrEmpty(s) 
                ? ctrVehicles.findByName(s)
                : CUltils.db.Vehicles.ToList();
            UpdateVehicleList(findVehicles);
        }

        private void dtgridVehicles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtgridVehicles.Rows[e.RowIndex];
            int vehicleID = Convert.ToInt32(row.Cells[0].Value);
            VehicleType vehicle = ctrvehicleType.GetVehiclesbyCategory(vehicleID);
            txtIDVehicle.DataBindings.Clear();
            txtIDVehicle.DataBindings.Add(new Binding("Text", vehicle, "IDVehicleType"));
            txtVehicleName.DataBindings.Clear();
            txtVehicleName.DataBindings.Add(new Binding("Text", vehicle, "VehicleTypeName"));
            txtManufacturer.DataBindings.Clear();
            txtManufacturer.DataBindings.Add(new Binding("Text", vehicle, "Manufacturer"));
            txtManufactureYear.DataBindings.Clear();
            txtManufactureYear.DataBindings.Add(new Binding("Text", vehicle, "ManufactureYear"));
        }
     
    }
}
