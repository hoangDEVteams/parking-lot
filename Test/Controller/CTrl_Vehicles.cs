using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Test.Controller
{
    internal class CTrl_Vehicles
    {
        public void upDate(Vehicle loaiSach)
        {
            CUltils.db.SaveChanges();
        }
        public void remove(Vehicle loaiSach)
        {
            CUltils.db.Vehicles.Remove(loaiSach);
            CUltils.db.SaveChanges();
        }
        public void add(Vehicle loaiSach)
        {
            CUltils.db.Vehicles.Add(loaiSach);
            CUltils.db.SaveChanges();
        }
    }
}
