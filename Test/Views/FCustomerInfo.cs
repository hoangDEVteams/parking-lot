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
    public partial class FCustomerInfo : Form
    {
        public FCustomerInfo()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                Ctrl_User ctrl_User = new Ctrl_User();
                Ctrl_Customer ctrl_Customer = new Ctrl_Customer();

                string gender = rdNam.Checked ? "Male" : "Female";

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
                    UserType = "Khách hàng"
                };

                var existingIdentityCard = CUltils.db.Users
                                    .FirstOrDefault(ic => ic.IdentityCard == newUser.IdentityCard);
                if (existingIdentityCard == null)
                {
                    CUltils.db.Users.Add(newUser);
                    CUltils.db.SaveChanges();

                    Customer newCustomer = new Customer
                    {
                        IDCustomer = ctrl_Customer.GenerateCustomerId(),
                        IDUser = newUser.IDUser
                    };

                    CUltils.db.Customers.Add(newCustomer);
                    CUltils.db.SaveChanges();

                    MessageBox.Show("Bạn đã thêm thông tin thành công!");
                }
                else
                {
                    MessageBox.Show("Khách hàng với CMND này đã tồn tại!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    }
}
