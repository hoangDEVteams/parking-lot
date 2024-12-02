using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.AddOn
{
    public class CRentalResult
    {
        public string RentalId { get; set; }
        public decimal RentPrice { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
