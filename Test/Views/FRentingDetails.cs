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
    public partial class FRentingDetails : Form
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
        public FRentingDetails(string rentalId, string rentalDate, string licensePlate, decimal rentPrice, int rentalDays, string customerId)
        {
            InitializeComponent();
            this.rentalId = rentalId;
            this.rentalDate = rentalDate;
            this.licensePlate = licensePlate;
            this.rentPrice = rentPrice;
            this.rentalDays = rentalDays;
            this.customerId = customerId;
            LoadCustomerDetails(customerId);
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
            // Gán thông tin vào các biến
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
            lblRentPrice.Text = rentPrice + "";
            lblRentDays.Text = rentalDays + "";

            lblTotalPer.Text = (rentPrice * rentalDays) + "";

            lblLicense.Text = licensePlate;
            LoadVehicleDetails(licensePlate);

            decimal totalPay = ((decimal)rentPrice * (int)rentalDays);
            lblTotalPay.Text = totalPay + "";
            decimal customerPay = 20000000;
            lblCusPay.Text = customerPay + " VND";
            lblChange.Text = (customerPay + totalPay) + "";
        }
        private void LoadVehicleDetails(string licensePlate)
        {
            CTrl_Vehicles vehicleController = new CTrl_Vehicles();
            var vehicle = vehicleController.GetVehicleByLicensePlate(licensePlate);

            if (vehicle != null)
            {
                lblCarName.Text = vehicle.Description; // Tên xe
                lblColor.Text = vehicle.Color;     // Màu sắc
                                                   // Bạn có thể hiển thị các thông tin khác nếu có
            }
            else
            {
                lblCarName.Text = "Vehicle not found";
                lblColor.Text = "N/A";
            }
        }
        
        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
