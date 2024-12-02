using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controller
{
    internal class Ctrl_Wallet
    {

        public static decimal GetUserBalance(string username)
        {
            var balance = (from acc in CUltils.db.Accounts
                           join wal in CUltils.db.Wallets on acc.IDAcc equals wal.IDAcc
                           where acc.Username == username
                           select wal.Money).FirstOrDefault();

            return balance;

        }
        public static decimal GetUserIDBalance(string userID)
        {
            var balance = (from acc in CUltils.db.Accounts
                           join wal in CUltils.db.Wallets on acc.IDAcc equals wal.IDAcc
                           where acc.IDUser == userID
                           select wal.Money).FirstOrDefault();

            return balance;
        }
        public static decimal? GetCusBalance(string cusID)
        {
            var balance = (from cus in CUltils.db.Customers
                           join user in CUltils.db.Users on cus.IDUser equals user.IDUser
                           join acc in CUltils.db.Accounts on user.IDUser equals acc.IDUser
                           join wal in CUltils.db.Wallets on acc.IDAcc equals wal.IDAcc
                           where cus.IDCustomer == cusID
                           select wal.Money).FirstOrDefault();

            return balance;
        }

    }
}
