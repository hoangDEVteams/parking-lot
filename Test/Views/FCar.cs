using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Controller;

namespace Test.Views
{
    public partial class FCar : Form
    {
        CTrl_Vehicles ctrVehicles = new CTrl_Vehicles();
        Ctrl_VehicleTypes ctrvehicleType = new Ctrl_VehicleTypes();
        V_VehicleData viewVehicle = new V_VehicleData();   
        public FCar()
        {
            InitializeComponent();
        }
        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVehicles();
        }
        private void UpdateVehicleList(List<V_VehicleData> viewVehicle)
        {
            dtgridVehicles.DataSource = viewVehicle;
            if (viewVehicle == null) {
                dtgridVehicles.Refresh();
            }

        }
        private void FilterVehicles()
        {
            string selectedStatus = cbStatus.SelectedItem?.ToString();
            string selectedColor = cbColor.SelectedItem?.ToString();

            List<V_VehicleData> filteredVehicles = CUltils.db.V_VehicleData.ToList();

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
        private void FCar_Load(object sender, EventArgs e)
        {
            using (var context = new BTXEntities1())
            {
                var data = context.V_VehicleData.ToList(); 
                dtgridVehicles.DataSource = data;         
            }

        }

        private void dtgridVehicles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtgridVehicles.Rows[e.RowIndex];
            int vehicleID = Convert.ToInt32(row.Cells[0].Value);
            VehicleType vehicle = ctrvehicleType.GetVehiclesbyCategory(vehicleID);
            lblCode.DataBindings.Clear();
            lblCode.DataBindings.Add(new Binding("Text", vehicle, "IDVehicleType"));
            lblName.DataBindings.Clear();
            lblName.DataBindings.Add(new Binding("Text", vehicle, "VehicleTypeName"));
            lblFacturer.DataBindings.Clear();
            lblFacturer.DataBindings.Add(new Binding("Text", vehicle, "Manufacturer"));
            lblYear.DataBindings.Clear();
            lblYear.DataBindings.Add(new Binding("Text", vehicle, "ManufactureYear"));
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            string s = txtFind.Text;
            if (string.IsNullOrEmpty(s))
            {
                FilterVehicles();
            }
            else
    {
                // Nếu ô tìm kiếm có chữ, lọc dữ liệu theo tìm kiếm
                List<V_VehicleData> filteredVehicles = CUltils.db.V_VehicleData
                    .Where(v => v.VehicleTypeName.ToLower().Contains(s.ToLower()))
                    .ToList();

                UpdateVehicleList(filteredVehicles);
            }
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVehicles();
        }
    }
}
