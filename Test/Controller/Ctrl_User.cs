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
            var users = CUltils.db.Users
                .Select(u => new
                {
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
        

    }
}
