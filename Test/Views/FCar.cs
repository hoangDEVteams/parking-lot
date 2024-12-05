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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Test.Views
{
    
    public partial class FCar : Form
    {
        private string IDUser;
        CTrl_Vehicles ctrVehicles = new CTrl_Vehicles();
        Ctrl_VehicleTypes ctrvehicleType = new Ctrl_VehicleTypes();
        V_VehicleData viewVehicle = new V_VehicleData();
        public FCar(string iDUser)
        {
            InitializeComponent();
            IDUser = iDUser;
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
        private void cbPrice_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FilterPrice();
        }
        public void FilterPrice() {
            decimal minPrice = 0, maxPrice = decimal.MaxValue;
            string selectedPrice = cbPrice.SelectedItem?.ToString();
            List<V_VehicleData> filteredVehicles;
            if (selectedPrice != null) {
                switch (selectedPrice) {
                    case "0-200k":
                        maxPrice = 200;
                        break;
                    case "200k-500k":
                        minPrice = 200; 
                        maxPrice = 500;
                        break;
                    case "trên 500k":
                        minPrice = 500;
                        maxPrice = 10000000;
                        break;
                }
                filteredVehicles = CUltils.db.V_VehicleData.Where(v => v.price >= minPrice && v.price <= maxPrice).ToList();
                UpdateVehicleList(filteredVehicles);
            }
            else
            {
                MessageBox.Show("no suitable vehicle","EROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
            dtgridVehicles.DataSource = ctrVehicles.VehicleData();
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
        private void cbPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVehicles();
        }

        private void btnCarRental_Click(object sender, EventArgs e)
        {

        }

        private void btnInfor_Click(object sender, EventArgs e)
        {
            if(lblCode.Text != null && lblCode.Text != "")
            {
                int idvehicle = Int32.Parse(lblCode.Text);
                FInforVehicle form = new FInforVehicle(idvehicle);
                form.ShowDialog();
            }
        }

        private void btnIncrese_Click(object sender, EventArgs e)
        {
            UpdateVehicleList(ctrVehicles.priceIncrese());
        }

        private void btnDecrese_Click(object sender, EventArgs e)
        {
            UpdateVehicleList(ctrVehicles.priceDerese());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            var data = ctrVehicles.VehicleData();
            UpdateVehicleList(data);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblFacturer_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private string customerIDPublic;
        private void btnCarRental_Click_1(object sender, EventArgs e)
        {
            Ctrl_Customer ctrl_Customer = new Ctrl_Customer();
            if (dtgridVehicles.SelectedRows.Count > 0)
            {

                DataGridViewRow selectedRow = dtgridVehicles.SelectedRows[0];

                string rentalId = selectedRow.Cells["ID"].Value.ToString();
                string rentalDate = DateTime.Now.ToString("yyyy-MM-dd");
                string licensePlate = selectedRow.Cells["LicensePlate"].Value.ToString();
                decimal rentPrice = (decimal)selectedRow.Cells["price"].Value;
                string status = "Renting";
                string currentEmployeeId = "NV001";
                string rentDay = Microsoft.VisualBasic.Interaction.InputBox(
                "Nhập số ngày thuê xe: ");


                if (int.TryParse(rentDay, out int rentalDays) && rentalDays > 0)
                {

                    string customerId = Ctrl_Customer.GetIDCusByIDUser(IDUser);
                    MessageBox.Show(customerId);
                    customerIDPublic = customerId;
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số ngày thuê hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                FRentingDetails newForm = new FRentingDetails(rentalId, rentalDate, licensePlate, rentPrice, rentalDays, customerIDPublic);
                Ctrl_Rental ctrl_Rental = new Ctrl_Rental();
                var result = ctrl_Rental.CreateRental(customerIDPublic, licensePlate, status, currentEmployeeId, rentalDays);
                newForm.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng trước khi thực hiện thuê xe.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void lblYear_Click(object sender, EventArgs e)
        {

        }

        private void lblCode_Click(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }
    }
}
