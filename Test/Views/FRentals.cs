using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
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

using Test.Views;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Test.Views
{
    public partial class FRentals : Form
    {
        private string username;
        public FRentals(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        private void LoadUserData()
        {
            Ctrl_User user = new Ctrl_User();
            var userData = user.UserData();
            Ctrl_Rental rental = new Ctrl_Rental();
            var rentalData = rental.RentalData();
            CTrl_Vehicles vhc = new CTrl_Vehicles();
            var vehicleData = vhc.getList();
            
            dtgvRentalData.DataSource = rentalData;

            dtgvUser.DataSource = userData;

            dtgvUser.CellContentClick += dtgvUser_CellContentClick;
            dtgvRentalData.ScrollBars = ScrollBars.Both;
            dtgvUser.ScrollBars = ScrollBars.Both;
            dtgvUserVehicle.ScrollBars = ScrollBars.Both;
            dtgvRentalData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dtgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dtgvUserVehicle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
            dtgvUser.CellContentClick += dtgvUser_CellContentClick;
            dtgvRentalData.ScrollBars = ScrollBars.Both;
            dtgvUser.ScrollBars = ScrollBars.Both;
            dtgvRentalData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
            dtgvUserVehicle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            List<string> validOptions = new List<string> { "1.Users", "2.User Vehicle", "3.All Rental"};

            // Gán danh sách vào ComboBox
            comboBox1.DataSource = validOptions;

            // Thiết lập DropDownStyle để người dùng chỉ chọn từ danh sách
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                string userId = dtgvUser.Rows[e.RowIndex].Cells["IDCustomer"].Value.ToString();
                LoadUserVehicles(userId);
               
            }
        }
        private void LoadUserVehicles(string customerId)
        {
            Ctrl_Rental rental = new Ctrl_Rental();

            var rentalData = rental.GetRentalDetailsByCustomer(customerId);


            dtgvUserVehicle.DataSource = rentalData;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (dtgvUser.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dtgvUser.SelectedRows[0];
                string customerId = selectedRow.Cells["IDCustomer"].Value.ToString();
                string userID = selectedRow.Cells["IDUser"].Value.ToString();
                var activeRentalsCount = CUltils.db.Rentals
                    .Count(r => r.IDCustomer == customerId && r.Status == "Renting");
                decimal userBalance = Ctrl_Wallet.GetUserIDBalance(userID);
                int IDAcc = Ctrl_Account.GetIDAccbyUserID(userID);

                MessageBox.Show($"User balance: {userBalance}");
                if (userBalance < 20000000)
                {
                    MessageBox.Show("Số dư không đủ để thuê xe. Vui lòng nạp thêm tiền.");
                    return;
                }
                if (activeRentalsCount >= 2)
                {
                    MessageBox.Show("Người dùng này đã thuê tối đa lượt.");
                    return;
                }

                CTrl_Vehicles vhc = new CTrl_Vehicles();
                var availableVehicles = vhc.getList();
                FChooseVehicles chooseVehiclesForm = new FChooseVehicles(availableVehicles);

                if (chooseVehiclesForm.ShowDialog() == DialogResult.OK)
                {
                    string selectedVehiclePlate = chooseVehiclesForm.SelectedVehiclePlate;
                    string selectedVehicleColor = chooseVehiclesForm.SelectedVehicleColor;
                    string selectedVehicleDescription = chooseVehiclesForm.SelectedVehicleDescription;

                    string inputDays = Interaction.InputBox(
                        "Input day:",
                        "Rent Date Information",
                        "1"
                    );

                    if (int.TryParse(inputDays, out int rentalDays) && rentalDays > 0)
                    {
                        string status = "Renting";
                        string currentEmployeeId = "NV001";

                        Ctrl_Rental rentalController = new Ctrl_Rental();
                        var rentalResult = rentalController.CreateRental(
                            customerId,
                            selectedVehiclePlate,
                            status,
                            currentEmployeeId,
                            rentalDays
                        );

                        if (rentalResult.Success)
                        {
                            string rentDate = DateTime.Now.ToString("dd/MM/yyyy");
                            Ctrl_Wallet.DespoitMoney(IDAcc);
                            MessageBox.Show($"Rental created successfully.\nVehicle Plate: {selectedVehiclePlate}\nColor: {selectedVehicleColor}\nDescription: {selectedVehicleDescription}");

                            FRentingDetails frenting = new FRentingDetails(
                                rentalResult.RentalId,
                                rentDate,
                                selectedVehiclePlate,
                                rentalResult.RentPrice,
                                rentalDays,
                                customerId
                            );
                            frenting.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show($"Failed to create rental. Error: {rentalResult.ErrorMessage}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Day!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select user.");
            }
        }


        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (dtgvUserVehicle.SelectedRows.Count > 0)
            {
                var selectedRow = dtgvUserVehicle.SelectedRows[0];

                string rentalId = selectedRow.Cells["IDRental"].Value?.ToString();
                string rentalDate = selectedRow.Cells["RentalDate"].Value?.ToString();
                string licensePlate = selectedRow.Cells["LicensePlate"].Value?.ToString();
                decimal rentPrice = (decimal)selectedRow.Cells["RentPrice"].Value;
                int rentalDays = (int)selectedRow.Cells["RentalDays"].Value;
                string customerID = selectedRow.Cells["IDCustomer"].Value?.ToString();
                string info = $"Rental ID: {rentalId}\n" +
                              $"Rental Date: {rentalDate}\n" +
                              $"License Plate: {licensePlate}\n" +
                              $"Rent Price: {rentPrice}\n" +
                              $"Rental Days: {rentalDays}";

                if (!string.IsNullOrEmpty(rentalId))
                {
                    Ctrl_Rental rentalController = new Ctrl_Rental();

                    bool result = rentalController.UpdateRentalStatus(rentalId, "Complete");

                    if (result)
                    {

                        MessageBox.Show("Rental status updated to Complete.");
                        FRentalDetails fRentalDetails = new FRentalDetails(
                            rentalId,
                            rentalDate,
                            licensePlate,
                            rentPrice,
                            rentalDays,
                            customerID
                        );
                        fRentalDetails.ShowDialog();
                        string customerId = dtgvUserVehicle.SelectedRows[0].Cells["IDCustomer"].Value.ToString();
                        LoadUserVehicles(customerId);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update rental status.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid rental ID.");
                }
            }
            else
            {
                MessageBox.Show("Please select a rental.");
            }
        }

        private void dtgvVehicle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgvUserVehicle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void FilterDataGridView(DataGridView dataGridView, string searchText)
        {
            CurrencyManager currencyManager = (CurrencyManager)BindingContext[dataGridView.DataSource];
            currencyManager.SuspendBinding();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                bool isVisible = false;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchText))
                    {
                        isVisible = true;
                        break; 
                    }
                }
                row.Visible = isVisible; 
            }

            currencyManager.ResumeBinding();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.ToLower();

            if (comboBox1.SelectedItem == null) return;

            string selectedTable = comboBox1.SelectedItem.ToString();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadUserData();
                dtgvRentalData.ScrollBars = ScrollBars.Both;
                dtgvUser.ScrollBars = ScrollBars.Both;
                dtgvRentalData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtgvUserVehicle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                return;
            }

            switch (selectedTable)
            {
                case "1.Users":
                    FilterDataGridView(dtgvUser, searchText);
                    break;
                case "2.User Vehicle":
                    FilterDataGridView(dtgvUserVehicle, searchText);
                    break;
                case "3.All Rental":
                    FilterDataGridView(dtgvRentalData, searchText);
                    break;
            }
        }
        }
    }
