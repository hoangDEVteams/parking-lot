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