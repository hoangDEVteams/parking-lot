﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
<<<<<<< Updated upstream
using Test.Controller;
=======
using System.Xml.Linq;
using Test.Controller;
using static System.Runtime.CompilerServices.RuntimeHelpers;
>>>>>>> Stashed changes

namespace Test.Views
{
    public partial class FCar_Admin : Form
    {
        CTrl_Vehicles ctrlVehicles = new CTrl_Vehicles();
        Ctrl_VehicleTypes ctrlVehicleType = new Ctrl_VehicleTypes();
        V_VehicleData viewVehicle = new V_VehicleData();
        Vehicle vehicle;
        public FCar_Admin()
        {
            InitializeComponent();
        }
        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVehicles();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVehicles();
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
        private void UpdateVehicleList(List<V_VehicleData> viewVehicle)
        {
            dtgridVehicles.DataSource = viewVehicle;
            if (viewVehicle == null)
            {
                dtgridVehicles.Refresh();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = dtgridVehicles.SelectedCells[0].RowIndex;
            if (index == -1)
            {
                ctrlVehicles.remove(vehicle);
            }
            else
            {
                MessageBox.Show("vui lòng chọn loại phương tiện","thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.v_VehicleDataTableAdapter1.FillBy(this.qLbaixeDataSet1.V_VehicleData);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillByToolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.v_VehicleDataTableAdapter.FillBy(this.qLbaixeDataSet.V_VehicleData);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void FCar_Admin_Load(object sender, EventArgs e)
        {
            dtgridVehicles.DataSource = ctrlVehicles.VehicleData().ToList();
        }

        private void dtgridVehicles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgridVehicles.Rows[e.RowIndex];
                int vehicleID = Convert.ToInt32(row.Cells[0].Value);
                VehicleType vehicle = ctrlVehicleType.GetVehiclesbyCategory(vehicleID);
                if (vehicle != null)
                {
                    txtCode.DataBindings.Clear();
                    txtCode.Text = vehicle.IDVehicleType.ToString();

                    txtName.DataBindings.Clear();
                    txtName.Text = vehicle.VehicleTypeName.ToString();

                    txtManu.DataBindings.Clear();
                    txtManu.Text = vehicle.Manufacturer;

                    txtYear.DataBindings.Clear();
                    txtYear.Text = vehicle.ManufactureYear.ToString();
                }
                else
                {
                    MessageBox.Show("Vehicle not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FCar_Admin_Load(object sender, EventArgs e)
        {
            dtgridVehicles.DataSource = ctrlVehicles.VehicleData().ToList();
        }

        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVehicles();
        }
        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVehicles();
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
                List<V_VehicleData> filteredVehicles = CUltils.db.V_VehicleData
                    .Where(v => v.VehicleTypeName.ToLower().Contains(s.ToLower()))
                    .ToList();

                UpdateVehicleList(filteredVehicles);
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
        private void UpdateVehicleList(List<V_VehicleData> viewVehicle)
        {
            dtgridVehicles.DataSource = viewVehicle;
            if (viewVehicle == null)
            {
                dtgridVehicles.Refresh();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = dtgridVehicles.SelectedCells[0].RowIndex;
            if (index == -1)
            {
                ctrlVehicles.remove(vehicle);
            }
            else
            {
                MessageBox.Show("vui lòng chọn loại phương tiện", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dtgridVehicles.CurrentRow == null)
            {
                MessageBox.Show("Please select a vehicle to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataGridViewRow row = dtgridVehicles.CurrentRow;
            if (row.Cells[0].Value == null || !int.TryParse(row.Cells[0].Value.ToString(), out int vehicleID))
            {
                MessageBox.Show("Invalid Vehicle ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FFillInforVehicle form = new FFillInforVehicle(vehicleID);
            form.ShowDialog();
        }

        private void dtgridVehicles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgridVehicles.Rows[e.RowIndex];
                int vehicleID = Convert.ToInt32(row.Cells[0].Value);
                VehicleType vehicle = ctrlVehicleType.GetVehiclesbyCategory(vehicleID);
                if (vehicle != null)
                {
                    txtCode.DataBindings.Clear();
                    txtCode.Text = vehicle.IDVehicleType.ToString();

                    txtName.DataBindings.Clear();
                    txtName.Text = vehicle.VehicleTypeName.ToString();

                    txtManu.DataBindings.Clear();
                    txtManu.Text = vehicle.Manufacturer;

                    txtYear.DataBindings.Clear();
                    txtYear.Text = vehicle.ManufactureYear.ToString();
                }
                else
                {
                    MessageBox.Show("Vehicle not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FFillInforVehicle form = new FFillInforVehicle();
            form.ShowDialog();
        }
    }
}
