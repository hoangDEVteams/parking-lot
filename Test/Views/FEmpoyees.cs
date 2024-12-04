using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Controller;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Test.Views
{
    public partial class FEmpoyees : Form
    {
        Ctrl_Employees ctrl_Employees = new Ctrl_Employees();
        Ctrl_User ctrlUser = new Ctrl_User();
        Employee employee = new Employee();
        Account account = new Account();
        Ctrl_Account ctrl_Account = new Ctrl_Account();

        public FEmpoyees()
        {
            InitializeComponent();
            
        }
        void load_Employees()
        {
            
            List<Employee> employees = ctrl_Employees.findAll();
            var dsNV = employees.Select(nv => new 
            {
                nv.User.birth,
               nv.IDUser,
                nv.IDEmployee,
                nv.User.Name,
                nv.User.PhoneNumber,
                nv.User.Gender,
                nv.User.Address,
                nv.User.IdentityCard,
                nv.User.BankNumber,
                nv.Position,
                nv.DateHired,
                nv.salary
            }).ToList();
            dataGridViewNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewNhanVien.DataSource = dsNV;
            dataGridViewNhanVien.ScrollBars = ScrollBars.Horizontal;
            
        }
        private void FEmpoyees_Load(object sender, EventArgs e)
        {
            Ctrl_Employees ctrl_Employees = new Ctrl_Employees();
            string taoIDNV = ctrl_Employees.taoIDNV();
            txtIDNhanVien.Text = taoIDNV;
            load_Employees();
        }

        private void dataGridViewNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridViewNhanVien.Rows[e.RowIndex];
            txtIDUser.Text = row.Cells["IDUser"].Value.ToString();
            txtIDNhanVien.Text = row.Cells["IDEmployee"].Value.ToString(); 
            txtTenNV.Text = row.Cells["Name"].Value.ToString(); 
            txtSDT.Text = row.Cells["PhoneNumber"].Value.ToString(); 
            txtBank.Text = row.Cells["BankNumber"].Value.ToString();
            txtCCCD.Text = row.Cells["IdentityCard"].Value.ToString();
            txtChucVu.Text = row.Cells["Position"].Value.ToString();
            txtLuong.Text = row.Cells["salary"].Value.ToString();
            txtDC.Text = row.Cells["Address"].Value.ToString();
            // Kiểm tra giá trị ngày sinh trong cơ sở dữ liệu
            if (row.Cells["birth"].Value != DBNull.Value && row.Cells["birth"].Value != null)
            {
                try
                {
                    datetimeBirth.Value = Convert.ToDateTime(row.Cells["birth"].Value);
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi chuyển đổi, gán ngày hiện tại làm giá trị mặc định
                    MessageBox.Show($"Lỗi khi chuyển đổi ngày sinh: {ex.Message}. Gán ngày mặc định.");
                    datetimeBirth.Value = DateTime.Now; // Gán ngày hiện tại
                }
            }
            else
            {
                // Nếu giá trị là NULL, gán ngày mặc định
                datetimeBirth.Value = DateTime.Now; // Gán ngày hiện tại
            }

            // Kiểm tra giá trị ngày vào làm trong cơ sở dữ liệu
            if (row.Cells["DateHired"].Value != DBNull.Value && row.Cells["DateHired"].Value != null)
            {
                try
                {
                    dateTimeVL.Value = Convert.ToDateTime(row.Cells["DateHired"].Value);
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi chuyển đổi, gán ngày hiện tại làm giá trị mặc định
                    MessageBox.Show($"Lỗi khi chuyển đổi ngày vào làm: {ex.Message}. Gán ngày mặc định.");
                    dateTimeVL.Value = DateTime.Now; // Gán ngày hiện tại
                }
            }
            else
            {
                // Nếu giá trị là NULL, gán ngày mặc định
                dateTimeVL.Value = DateTime.Now; // Gán ngày hiện tại
            }




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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //string gioitinh = rdNam.Checked ? "Male" : "Female";
            //string role = txtChucVu.Text == "Admin" ? "Admin" : "NV";
            //string taoIDND = ctrlUser.taoIDNV();
            //User user = new User()
            //{

            //    IDUser = taoIDND,
            //    Name = txtTenNV.Text,
            //    PhoneNumber = txtSDT.Text,
            //    Address = txtDC.Text,
            //    BankNumber = txtBank.Text,
            //    IdentityCard = txtCCCD.Text,
            //    Gender = gioitinh,
            //    UserType = role,
            //    birth = datetimeBirth.Value,
            //};
            //string email = txtEmail.Text; 
            //if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            //{
            //    MessageBox.Show("Vui lòng nhập email hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return; 
            //}
            //var existingIdentityCards = CUltils.db.Users
            //  .FirstOrDefault(id => id.IdentityCard == user.IdentityCard);
            //var existingEmail = CUltils.db.Accounts
            //              .FirstOrDefault(u => u.Email == email);
            //var existingPhoneNumber = CUltils.db.Users
            //              .FirstOrDefault(u => u.PhoneNumber == user.PhoneNumber);
            //var existingBankNumber = CUltils.db.Users
            //              .FirstOrDefault(u => u.BankNumber == user.BankNumber);
            //if (existingIdentityCards == null && existingEmail == null && existingPhoneNumber == null && existingBankNumber == null)
            //{
               
            //    CUltils.db.Users.Add(user);
            //    CUltils.db.SaveChanges();
            //    string newEmployeeId = ctrl_Employees.taoIDNV();
             
            //    int newAccountId = ctrl_Account.taoACCNV(); 
            //    string password = ctrl_Account.GenerateRandomPassword();
            //    string salt = CPass.GenerateSalt();
               
            //    string username = "NV" + newEmployeeId.Substring(newEmployeeId.Length - 4);
            //    string verificationCode = ctrl_Account.GenerateRandomVerificationCode();
            //    Account newAccount = new Account
            //    {
            //        IDAcc = newAccountId,
            //        Username = username,
            //        Password = password, 
            //        Role = role,
            //        Status = "Active",
            //        VerificationCodeExpiration = DateTime.Now,
            //        Email = email,
            //        IDUser = user.IDUser,
            //        Salt = salt,
            //        VerificationCode = verificationCode
            //    };
               
            //    CUltils.db.Accounts.Add(newAccount);
            //    CUltils.db.SaveChanges();

                    


            //    Employee newEmployee = new Employee
            //    {
            //        IDEmployee = ctrl_Employees.taoIDNV(), 
            //        IDUser = user.IDUser,
            //        Position = txtChucVu.Text,
            //        DateHired = DateTime.Now, 
            //        salary = decimal.TryParse(txtLuong.Text, out decimal salary) ? salary : 0 
            //    };

            //    CUltils.db.Employees.Add(newEmployee);
            //    CUltils.db.SaveChanges();

            //    MessageBox.Show($"Thêm nhân viên thành công!\nTài khoản: {username}\nMật khẩu: {password}", "Thông báo");
            //}


            //else if (existingIdentityCards != null)
            //{
            //    MessageBox.Show("CCCD này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //else if (existingEmail != null)
            //{
            //    MessageBox.Show("Email này đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //else if (existingPhoneNumber != null)
            //{
            //    MessageBox.Show("Số điện thoại này đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //else if (existingBankNumber != null)
            //{
            //    MessageBox.Show("Số ngân hàng này đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

                
            
            //else
            //{
            //    MessageBox.Show("Số CCCD, Email, Số điện thoại và Số ngân hàng này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            //}

           



            //load_Employees();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridViewNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedUserID = dataGridViewNhanVien.SelectedRows[0].Cells["IDUser"].Value.ToString();
            string selectedEmployeeID = dataGridViewNhanVien.SelectedRows[0].Cells["IDEmployee"].Value.ToString();

            User updatedUser = new User()
            {
                IDUser = selectedUserID,
                Name = txtTenNV.Text,
                PhoneNumber = txtSDT.Text,
                Address = txtDC.Text,
                BankNumber = txtBank.Text,
                Gender = rdNam.Checked ? "Male" : "Female",
                birth = datetimeBirth.Value 
            };

           
            Employee updatedEmployee = new Employee()
            {
                IDEmployee = selectedEmployeeID,
                IDUser = selectedUserID,
                Position = txtChucVu.Text,
                DateHired = dateTimeVL.Value 
            };

         
            string newRole = txtChucVu.Text == "Admin" ? "Admin" : "NV";

        
            ctrlUser.UpdateEmployeeInfoAndRole(updatedUser, updatedEmployee, newRole);

          
            load_Employees();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedID = dataGridViewNhanVien.SelectedRows[0].Cells["IDUser"].Value.ToString();

            DialogResult confirm = MessageBox.Show($"Bạn có chắc muốn xóa người dùng ID: {selectedID}?",
                                                   "Xác nhận",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                ctrlUser.DeleteUserAndRelatedData(selectedID);
                load_Employees(); // Tải lại danh sách sau khi xóa
            }
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtIDUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtTenNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }

        private void txtChucVu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }

        private void txtLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true;  
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true;  
            }
        }

        private void txtCCCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            txtIDUser.Clear();
            txtIDNhanVien.Clear();
            txtTenNV.Clear();                
            txtSDT.Clear();
            txtBank.Clear();
            txtCCCD.Clear();
            txtChucVu.Clear();
            txtLuong.Clear();
            txtDC.Clear();

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            


        }
    }

        
    }
