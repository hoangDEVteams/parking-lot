using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test.Controller
{
    internal class Ctrl_User
    {
        private static string GenerateNextID(string currentMaxID)
        {
            if (string.IsNullOrEmpty(currentMaxID))
            {
                return "NV001";
            }

            string prefix = currentMaxID.Substring(0, 2); 
            int number = int.Parse(currentMaxID.Substring(2));

            number++;
            return $"{prefix}{number:D3}";
        }
        public static void UpdateSalaryForEmp(decimal salary, string IDUser)
        {
            var em = CUltils.db.Employees.SingleOrDefault(e => e.IDUser == IDUser);
            if (em != null)
            {
                em.salary = salary;
                CUltils.db.SaveChanges();
            }
        }
        public static void UpdateRoleUserEm(int IDAcc, string position)
        {
            var user = CUltils.db.Users.Include("Employees").SingleOrDefault(u => u.IDAcc == IDAcc);
            if (user != null)
            {
                user.UserType = "Nhân Viên";

                string idUser = user.IDUser;

                Employee emp = user.Employees.FirstOrDefault();
                if (emp != null)
                {
                    emp.Position = position;
                }
                else
                {
                    string maxID = CUltils.db.Employees
                        .OrderByDescending(e => e.IDEmployee)
                        .Select(e => e.IDEmployee)
                        .FirstOrDefault();

                    string newID = GenerateNextID(maxID);

                    emp = new Employee
                    {
                        IDEmployee = newID, 
                        IDUser = idUser,   
                        DateHired = DateTime.Now,
                        Position = position 
                    };

                    user.Employees.Add(emp);
                }

                CUltils.db.SaveChanges();
            }
            else
            {
                throw new Exception($"Không tìm thấy user với IDAcc: {IDAcc}");
            }
        }
        public static void UpdateRoleUserCus(int IDAcc)
        {
            var user = CUltils.db.Users.SingleOrDefault(u => u.IDAcc == IDAcc);
            if (user != null)
            {
                user.UserType = "Khách Hàng";
                CUltils.db.SaveChanges();
            }
        }
        public static void UpdateRoleUserAD(int IDAcc)
        {
            var user = CUltils.db.Users.SingleOrDefault(u => u.IDAcc == IDAcc);
            if (user != null)
            {
                user.UserType = "Admin";
                CUltils.db.SaveChanges();
            }
        }
        public List<object> GetUserByIDAcc(int IDAcc)
        {
            var users = CUltils.db.Users
                .Where(u => u.Accounts.Any(a => a.IDAcc == IDAcc))  
                .Select(u => new
                {
                    Money = CUltils.db.Wallets
                                    .Where(w => w.IDAcc == IDAcc) 
                                    .Select(w => w.Money)
                                    .FirstOrDefault(), 
                    u.IDUser,
                    u.Name,
                    u.Gender,
                    u.birth,
                    u.PhoneNumber,
                    u.BankNumber,
                    u.Address,
                    u.IdentityCard
                })
                .ToList();

            return users.Cast<object>().ToList();
        }

        public List<object> SearchUserByName(string name)
        {
            try
            {
                var users = CUltils.db.Users.Include("Customers")
                    .Where(u => u.Name.ToLower().Contains(name.ToLower()))
                    .Select(u => new
                    {
                        IDCustomer = u.Customers.FirstOrDefault().IDCustomer,
                        u.IDUser,
                        u.Name,
                        u.Gender,
                        u.PhoneNumber,
                        u.Address,
                        u.IdentityCard
                    })
                    .ToList();

                return users.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi tìm kiếm: {ex.Message}");
                return new List<object>();
            }
        }

        public List<object> UserData()
        {
            var users = CUltils.db.Users.Include("Customers")
                .Select(u => new
                {
                    IDCustomer = u.Customers.FirstOrDefault().IDCustomer,
                    u.IDUser,
                    u.Name,
                    u.Gender,
                    u.PhoneNumber,
                    u.Address,
                    u.IdentityCard
                })
                .ToList();

            return users.Cast<object>().ToList();
        }
        public void AddUser(User newUser)
        {
            CUltils.db.Users.Add(newUser);
            CUltils.db.SaveChanges();
        }

        public void RemoveUser(string userId)
        {
            var user = CUltils.db.Users.SingleOrDefault(u => u.IDUser == userId);
            if (user != null)
            {
                CUltils.db.Users.Remove(user);
                CUltils.db.SaveChanges();
            }
        }

        public void UpdateUser(User updatedUser)
        {
            var user = CUltils.db.Users.SingleOrDefault(u => u.IDUser == updatedUser.IDUser);
            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.Gender = updatedUser.Gender;
                user.PhoneNumber = updatedUser.PhoneNumber;
                user.Address = updatedUser.Address;
                user.IdentityCard = updatedUser.IdentityCard;
                CUltils.db.SaveChanges();
            }
        }
        public User GetUserByUser(string username)
        {
            var user = CUltils.db.Accounts
                .FirstOrDefault(c => c.Username == username);
            return CUltils.db.Users.FirstOrDefault(u => u.IDUser == user.IDUser);
        }
        public string GenerateUserId()
        {
            var maxID = CUltils.db.Users
                .Select(u => u.IDUser)
                .OrderByDescending(id => id)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(maxID))
            {
                return "U001";
            }

            string numericPart = maxID.StartsWith("U") ? maxID.Substring(1) : maxID;
            int number;

            if (int.TryParse(numericPart, out number))
            {
                number++;
                return "U" + number.ToString("D3");
            }

            throw new Exception("Không thể tạo ID mới do định dạng không hợp lệ.");
        }
    }
}
