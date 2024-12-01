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

    }
}
