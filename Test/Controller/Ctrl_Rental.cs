using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controller
{
    internal class Ctrl_Rental
    {
        public List<object> RentalData()
        {
            var rental = CUltils.db.Rentals.Include("RentalDetails").Select(r => new
                {
                    r.RentalDate,
                    LicensePlate = r.RentalDetails.Select(rd => rd.LicensePlate).FirstOrDefault(),
                    RentPrice = r.RentalDetails.Select(rd => rd.RentPrice).FirstOrDefault(),
                    RentalDays = r.RentalDetails.Select(rd => rd.RentalDays).FirstOrDefault(),

            })
                .ToList();

            return rental.Cast<object>().ToList();
        }
    }
}
