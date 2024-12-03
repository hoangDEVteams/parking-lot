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
        private string IDUser;
        public FCustomerInfo(string IDUser)
        {
            InitializeComponent();
            this.IDUser = IDUser;
        }
        private void LoadUserInfo()
        {
            try
            {
                var user = CUltils.db.Users.SingleOrDefault(u => u.IDUser == IDUser);
                if (user != null)
                {
                    txtTenKH.Text = user.Name;
                    txtSDT.Text = user.PhoneNumber;
                    txtDiaChi.Text = user.Address;
                    txtTKNH.Text = user.BankNumber;
                    txtCMND.Text = user.IdentityCard;
                    dateTimePicker1.Value = user.birth.Value;
                    if (user.Gender == "Male")
                    {
                        rdNam.Checked = true;
                    }
                    else
                    {
                        rdNu.Checked = true;
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin: {ex.Message}");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string gender = rdNam.Checked ? "Male" : "Female";

                var userToUpdate = CUltils.db.Users.SingleOrDefault(u => u.IDUser == IDUser);
                if (userToUpdate != null)
                {
                    userToUpdate.Name = txtTenKH.Text;
                    userToUpdate.PhoneNumber = txtSDT.Text;
                    userToUpdate.Address = txtDiaChi.Text;
                    userToUpdate.BankNumber = txtTKNH.Text;
                    userToUpdate.IdentityCard = txtCMND.Text;
                    userToUpdate.birth = dateTimePicker1.Value;
                    userToUpdate.Gender = gender;

                    CUltils.db.SaveChanges();

                    MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng để cập nhật!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void FCustomerInfo_Load(object sender, EventArgs e)
        {
            LoadUserInfo();
        }
    }
}
