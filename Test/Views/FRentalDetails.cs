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
    
    public partial class FRentalDetails : Form
    {
        private string rentalId;
        private string rentalDate;
        private string licensePlate;
        private decimal rentPrice;
        private int rentalDays;
        private string customerId;


        private string customerName;
        private string customerPhone;
        private string customerAddress;
        private string membershipLevel;
        private string gender;          // Giới tính
        private string identityCard;    // CMND/CCCD
        private string bankNumber;
        private DateTime birthday;
        public FRentalDetails(string rentalId, string rentalDate, string licensePlate, decimal rentPrice, int rentalDays, string customerId)
        {
            InitializeComponent();

            // Lưu thông tin vào các biến
            this.rentalId = rentalId;
            this.rentalDate = rentalDate;
            this.licensePlate = licensePlate;
            this.rentPrice = rentPrice;
            this.rentalDays = rentalDays;
            this.customerId = customerId;
            LoadCustomerDetails(customerId);
        }
        private void LoadVehicleDetails(string licensePlate)
        {
            CTrl_Vehicles vehicleController = new CTrl_Vehicles();
            var vehicle = vehicleController.GetVehicleByLicensePlate(licensePlate);

            if (vehicle != null)
            {
                lblCarName.Text = vehicle.Description; 
                lblColor.Text = vehicle.Color;     
            }
            else
            {
                lblCarName.Text = "Vehicle not found";
                lblColor.Text = "N/A";
            }
        }

        private void LoadCustomerDetails(string customerId)
        {
            Ctrl_Customer customerController = new Ctrl_Customer();
            var customerDetails = customerController.GetCustomerDetailsById(customerId);
            if (customerDetails.BirthDay.HasValue)
            {
                lblUserBirth.Text = customerDetails.BirthDay.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                lblUserBirth.Text = "Unknown";
            }
            customerName = customerDetails.Name;
            customerPhone = customerDetails.PhoneNumber;
            customerAddress = customerDetails.Address;
            membershipLevel = customerDetails.MembershipLevel;
            gender = customerDetails.Gender;
            identityCard = customerDetails.IdentityCard;
            bankNumber = customerDetails.BankNumber;
            

            // Hiển thị thông tin trên form
            lblCusName.Text = customerName;
            lblCusPhone.Text = customerPhone;
            lblCusAddress.Text = customerAddress;
            lblMBShip.Text = membershipLevel;
            lblCusBank.Text = bankNumber;
            lblCusIden.Text = identityCard;
            lblDateBill.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblRentalId.Text = rentalId;
            lblGender.Text = gender;
            lblRentday.Text = rentalDate;
            lblRentPrice.Text = rentPrice+"";
            lblRentDays.Text = rentalDays+"";

            lblTotalPer.Text = (rentPrice * rentalDays) + "";

            lblLicense.Text = licensePlate;
            LoadVehicleDetails(licensePlate);
            LoadPenaltiesByCustomerId(customerId);
            lblTotalPenalty.Text = CalculateTotalPenalties().ToString("C");

            decimal totalPay = ((decimal)rentPrice * (int)rentalDays + CalculateTotalPenalties());
            lblTotalPay.Text = totalPay + "";
            decimal customerPay = 20000000;
            lblCusPay.Text = customerPay + "";
            lblChange.Text = (customerPay - totalPay) + "";
        }
        private decimal CalculateTotalPenalties()
        {
            decimal totalPenalties = 0;
            foreach (ListViewItem item in lsvPenalties.Items)
            {
                totalPenalties += decimal.Parse(item.SubItems[4].Text, System.Globalization.NumberStyles.Currency);
            }
            return totalPenalties;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
            lblRentday.Text = rentalDate;

        }
        public void LoadPenaltiesByCustomerId(string customerId)
        {
            if (lsvPenalties.Columns.Count == 0)
            {
                lsvPenalties.Columns.Add("Penalty ID", 100);
                lsvPenalties.Columns.Add("Penalty Date", 150);
                lsvPenalties.Columns.Add("Employee ID", 150);
                lsvPenalties.Columns.Add("Reason", 300);
                lsvPenalties.Columns.Add("Price", 250);
            }

            Ctrl_Penalties penaltyController = new Ctrl_Penalties();

            // Get penalties
            var penalties = penaltyController.GetPenaltiesByCustomerId(customerId);
            lsvPenalties.Items.Clear();

            if (penalties != null)
            {
                foreach (var penalty in penalties)
                {
                    ListViewItem item = new ListViewItem(penalty.IDPenalty.ToString());
                    item.SubItems.Add(penalty.PenaltyDate.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(penalty.IDEmployee);

                    var penaltyDetails = penaltyController.GetPenaltyDetailsByPenaltyId(penalty.IDPenalty);
                    if (penaltyDetails != null && penaltyDetails.Any())
                    {
                        item.SubItems.Add(penaltyDetails.First().Reason); // Chỉ lấy lý do đầu tiên
                        item.SubItems.Add(penaltyDetails.First().price.ToString("C"));
                    }
                    else
                    {
                        item.SubItems.Add("N/A");
                        item.SubItems.Add("0");
                    }
                    lsvPenalties.Items.Add(item);
                }
            }
        }


        private void lblRentPrice_Click(object sender, EventArgs e)
        {

        }

        private void FRentalDetails_Load(object sender, EventArgs e)
        {
            lsvPenalties.View = View.Details;

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void lsvPenalties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblTotalPenalty_Click(object sender, EventArgs e)
        {

        }

        private void lblCusPay_Click(object sender, EventArgs e)
        {

        }
    }
}
