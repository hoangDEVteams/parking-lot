using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Test.Controller
{
    internal class Ctrl_Customer
    {
        public List<Customer> getList()
        {
            return CUltils.db.Customers.ToList();
        }

        public string GenerateCustomerId()
        {
            var maxID = CUltils.db.Customers
                .Select(c => c.IDCustomer)
                .OrderByDescending(id => id)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(maxID))
            {
                return "KH001";
            }

            string numericPart = maxID.StartsWith("KH") ? maxID.Substring(2) : maxID;
            int number;

            if (int.TryParse(numericPart, out number))
            {
                number++;
                return "KH" + number.ToString("D3");
            }

            throw new Exception("Không thể tạo ID mới do định dạng không hợp lệ.");
        }

        public void AddCustomer(Customer customer)
        {
            customer.IDCustomer = GenerateCustomerId();

            CUltils.db.Customers.Add(customer);
            CUltils.db.SaveChanges();
        }

        public void RemoveCustomer(string customerId)
        {
            try
            {
                var customer = CUltils.db.Customers.SingleOrDefault(c => c.IDCustomer == customerId);
                if (customer != null)
                {
                    var user = CUltils.db.Users.SingleOrDefault(u => u.IDUser == customer.IDUser);
                    var idacc = CUltils.db.Accounts.SingleOrDefault(a => a.IDUser == customer.IDUser);
                    CUltils.db.Customers.Remove(customer);

                    if (user != null)
                    {
                        CUltils.db.Users.Remove(user);
                        CUltils.db.Accounts.Remove(idacc);
                    }

                    CUltils.db.SaveChanges();
                }
                else
                {
                    throw new Exception($"Không tìm thấy khách hàng với ID: {customerId}");
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Lỗi khi xóa khách hàng: {ex.Message}");
            }
        }
        public static string GetIDCusByIDUser(string IDUser)
        {
            var cus = CUltils.db.Customers.SingleOrDefault(a => a.IDUser == IDUser);
            if (cus != null)
            {
                return cus.IDCustomer;
            }
            return null;
        }
        public void UpdateCustomer(string idCustomer, User updatedUser)
        {
            var customer = CUltils.db.Customers.SingleOrDefault(c => c.IDCustomer == idCustomer);

            if (customer == null)
            {
                throw new Exception("Không tìm thấy Customer với IDCustomer đã cung cấp.");
            }

            var user = CUltils.db.Users.SingleOrDefault(u => u.IDUser == customer.IDUser);

            if (user == null)
            {
                throw new Exception("Không tìm thấy User liên kết với Customer.");
            }

            user.Name = updatedUser.Name;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.Address = updatedUser.Address;
            user.IdentityCard = updatedUser.IdentityCard;

            CUltils.db.SaveChanges();
        }
        public (string Name, string PhoneNumber, string Address, string MembershipLevel, string Gender, string IdentityCard, string BankNumber, DateTime? BirthDay) GetCustomerDetailsById(string customerId)
        {
            try
            {
                var customerInfo = (from customer in CUltils.db.Customers
                                    join user in CUltils.db.Users
                                    on customer.IDUser equals user.IDUser
                                    where customer.IDCustomer == customerId
                                    select new
                                    {
                                        Name = user.Name,
                                        PhoneNumber = user.PhoneNumber,
                                        Address = user.Address,
                                        MembershipLevel = customer.MembershipLevel,
                                        Gender = user.Gender,
                                        IdentityCard = user.IdentityCard,
                                        BankNumber = user.BankNumber,
                                        BirthDay = user.birth
                                    }).FirstOrDefault();

                if (customerInfo == null)
                {
                    throw new Exception("Customer not found.");
                }

                return (
                    customerInfo.Name,
                    customerInfo.PhoneNumber,
                    customerInfo.Address,
                    customerInfo.MembershipLevel ?? "N/A",
                    customerInfo.Gender,
                    customerInfo.IdentityCard,
                    customerInfo.BankNumber,
                    customerInfo.BirthDay
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return (null, null, null, null, null, null, null, null);
            }
        }
    }
}