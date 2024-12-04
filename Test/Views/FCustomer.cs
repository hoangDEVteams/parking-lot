using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Controller;

namespace Test.Views
{
    public partial class FCustomer : Form
    {
        public FCustomer()
        {
            InitializeComponent();
        }

        private void FCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                Ctrl_Customer ctrl_Customer = new Ctrl_Customer();

                string newIDCustomer = ctrl_Customer.GenerateCustomerId();

                txtMaKH.Text = newIDCustomer;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo IDCustomer: {ex.Message}");
            }
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            Ctrl_Customer customerService = new Ctrl_Customer();
            List<Customer> customer = customerService.getList();
            var customerData = customer
                .Where(c => c.User.UserType == "Khách Hàng") 
                .Select(c => new
                {
                    c.IDCustomer,
                    c.User.Name,
                    c.User.PhoneNumber,
                    c.User.Gender,
                    c.User.birth,
                    c.User.Address,
                    c.User.BankNumber,
                    c.User.IdentityCard,
                }).ToList();

            dtgridDSKH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgridDSKH.DataSource = customerData;
            dtgridDSKH.ScrollBars = ScrollBars.Horizontal;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
        }

        private void dtgridDSKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgridDSKH.SelectedRows.Count > 0)
                {
                    string customerId = dtgridDSKH.SelectedRows[0].Cells["IDCustomer"].Value.ToString();

                    var confirmResult = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa khách hàng với ID: {customerId} không?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes)
                    {
                        Ctrl_Customer ctrl_Customer = new Ctrl_Customer();
                        ctrl_Customer.RemoveCustomer(customerId);

                        MessageBox.Show("Xóa khách hàng thành công!");

                        LoadCustomerData();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một khách hàng để xóa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string IDCustomer = txtMaKH.Text;

                User updatedUser = new User
                {
                    Name = txtTenKH.Text,
                    PhoneNumber = txtSDT.Text,
                    Address = txtDiaChi.Text,
                    IdentityCard = txtCMND.Text
                };

                Ctrl_Customer ctrl_Customer = new Ctrl_Customer();
                ctrl_Customer.UpdateCustomer(IDCustomer, updatedUser);

                MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadCustomerData();

        
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                Ctrl_User ctrl_User = new Ctrl_User();
                Ctrl_Customer ctrl_Customer = new Ctrl_Customer();
                Ctrl_Account ctrl_Account = new Ctrl_Account();

                string gender = rdNam.Checked ? "Male" : "Female";
                string email = txtEmail.Text.Trim();

                string fullName = txtTenKH.Text.Trim();

                string[] nameParts = fullName.Split(' ');
                string username = nameParts.Last();

                int suffix = 1;
                while (CUltils.db.Accounts.Any(a => a.Username == username))
                {
                    username = nameParts.Last() + suffix.ToString();
                    suffix++;
                }

                string password = GenerateRandomPassword();
                string salt = CPass.GenerateSalt();
                string hashPassword = CPass.HashPasswordWithSalt(password, salt);

                var newAccount = new Account
                {
                    Email = email,
                    Username = username,
                    Password = hashPassword,
                    Role = "Khách Hàng",
                    Status = "Active",
                    Salt = salt
                };

                CUltils.db.Accounts.Add(newAccount);
                CUltils.db.SaveChanges();

                int newAccountId = newAccount.IDAcc;

                Ctrl_Account.CreateWalletForAccount(newAccountId);

                string newUserId = ctrl_User.GenerateUserId();

                User newUser = new User
                {
                    IDUser = newUserId,
                    Name = txtTenKH.Text,
                    PhoneNumber = txtSDT.Text,
                    Address = txtDiaChi.Text,
                    BankNumber = txtTKNH.Text,
                    IdentityCard = txtCMND.Text,
                    birth = dateTimePicker1.Value,
                    Gender = gender,
                    UserType = "Khách Hàng",
                    IDAcc = newAccountId
                };

                var existingIdentityCard = CUltils.db.Users
                                        .FirstOrDefault(ic => ic.IdentityCard == newUser.IdentityCard);
                if (existingIdentityCard != null)
                {
                    MessageBox.Show("Khách hàng với CMND này đã tồn tại!");
                    return;
                }

                CUltils.db.Users.Add(newUser);
                CUltils.db.SaveChanges();

                newAccount.IDUser = newUserId;
                CUltils.db.SaveChanges();

                Customer newCustomer = new Customer
                {
                    IDCustomer = ctrl_Customer.GenerateCustomerId(),
                    IDUser = newUser.IDUser,
                    MembershipLevel = "Bronze",
                    Points = 0
                };

                CUltils.db.Customers.Add(newCustomer);
                CUltils.db.SaveChanges();

                MessageBox.Show("Thêm khách hàng thành công!");

                Ctrl_Account.SendPasswordEmail(email, password, username, fullName);

                LoadCustomerData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
        private string GenerateRandomPassword()
        {
            Random random = new Random();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string digits = "0123456789";

            string randomLetters = new string(Enumerable.Range(0, 3)
                .Select(_ => letters[random.Next(letters.Length)])
                .ToArray());

            string randomDigits = new string(Enumerable.Range(0, 3)
                .Select(_ => digits[random.Next(digits.Length)])
                .ToArray());

            return randomLetters + randomDigits;
        }
        private void dtgridDSKH_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtgridDSKH.Rows[e.RowIndex];

            txtMaKH.Text = row.Cells["IDCustomer"].Value.ToString();
            txtTenKH.Text = row.Cells["Name"].Value.ToString();
            txtSDT.Text = row.Cells["PhoneNumber"].Value.ToString();
            txtDiaChi.Text = row.Cells["Address"].Value.ToString();
            txtCMND.Text = row.Cells["IdentityCard"].Value.ToString();
            txtTKNH.Text = row.Cells["BankNumber"].Value.ToString();
            string gender = row.Cells["Gender"].Value.ToString();

            if (gender == "Male")
            {
                rdNam.Checked = true;
            }
            else if (gender == "Female")
            {
                rdNu.Checked = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Ctrl_Customer ctrl_Customer = new Ctrl_Customer();
            string newIDCustomer = ctrl_Customer.GenerateCustomerId();

            txtMaKH.Text = newIDCustomer;
            txtTenKH.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtCMND.Text= string.Empty;
            txtTKNH.Text = string.Empty;
            txtDiaChi.Text= string.Empty;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rdNu_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdNam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtTKNH_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            if (txtSDT.Text.Length > 10)
            {
                txtSDT.Text = txtSDT.Text.Substring(0, 10);
            }
        }
        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCMND_TextChanged(object sender, EventArgs e)
        {
            if (txtCMND.Text.Length > 12)
            {
                txtCMND.Text = txtCMND.Text.Substring(0, 12);
            }
        }

        private void txtTenKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string searchName = txtTimKiem.Text;
                Ctrl_User ctrlUser = new Ctrl_User();

                var searchResults = ctrlUser.SearchUserByName(searchName);

                dtgridDSKH.DataSource = searchResults;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
