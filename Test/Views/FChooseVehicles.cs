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

namespace Test.Views
{
    public partial class FChooseVehicles : Form
    {

        public string SelectedVehiclePlate { get; private set; }
        public string SelectedVehicleColor { get; private set; }
        public string SelectedVehicleDescription { get; private set; }
        public FChooseVehicles(List<object> availableVehicles)
        {
            InitializeComponent();
            dtgvAvailableVehicles.DataSource = availableVehicles;
            dtgvAvailableVehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvAvailableVehicles.CellContentClick += dtgvAvailableVehicles_CellContentClick;
        }

        private void FChooseVehicles_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dtgvAvailableVehicles.Columns)
            {
                cbxFilter.Items.Add(column.HeaderText);
            }
        }

        private void dtgvAvailableVehicles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string vehiclePlate = dtgvAvailableVehicles.Rows[e.RowIndex].Cells["LicensePlate"].Value.ToString();
                string vhStatus = dtgvAvailableVehicles.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                string color = dtgvAvailableVehicles.Rows[e.RowIndex].Cells["Color"].Value.ToString();
                string description = dtgvAvailableVehicles.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                string vehicleTypeName = dtgvAvailableVehicles.Rows[e.RowIndex].Cells["VehicleTypeName"].Value.ToString();

                if (vhStatus == "Unavailable")
                {
                    MessageBox.Show("This vehicle is currently rented. Please select another vehicle.");
                    return;
                }
                else
                {
                    DialogResult result = MessageBox.Show($"Are you sure you want to select vehicle {vehiclePlate}?", "Confirm Selection", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        SelectedVehiclePlate = vehiclePlate;
                        SelectedVehicleColor = color;
                        SelectedVehicleDescription = description;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                
            }
        }

        private void cbxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string selectedColumn = cbxFilter.SelectedItem.ToString();
            string filterValue = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(selectedColumn) || string.IsNullOrEmpty(filterValue))
            {
                MessageBox.Show("Vui lòng chọn cột và nhập giá trị lọc.");
                return;
            }

            var filteredData = from row in dtgvAvailableVehicles.Rows.Cast<DataGridViewRow>()
                               where row.Cells[selectedColumn].Value != null &&
                                     row.Cells[selectedColumn].Value.ToString().Contains(filterValue)
                               select new
                               {
                                   LicensePlate = row.Cells["LicensePlate"].Value.ToString(),
                                   VehicleTypeName = row.Cells["VehicleTypeName"].Value.ToString(),
                                   Color = row.Cells["Color"].Value.ToString(),
                                   Status = row.Cells["Status"].Value.ToString(),
                                   Description = row.Cells["Description"].Value.ToString(),
                                   IDEmployee = row.Cells["IDEmployee"].Value.ToString(),
                                   Manufacture = row.Cells["Manufacture"].Value.ToString(),
                                   ManufactureYear = row.Cells["ManufactureYear"].Value.ToString()
                               };

            dtgvAvailableVehicles.DataSource = filteredData.ToList();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            CTrl_Vehicles vhc = new CTrl_Vehicles();
            var availableVehicles = vhc.getList();
            cbxFilter.SelectedIndex = -1;
            txtSearch.Clear();
            dtgvAvailableVehicles.DataSource = availableVehicles;
            dtgvAvailableVehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
