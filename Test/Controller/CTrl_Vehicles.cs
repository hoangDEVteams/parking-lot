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
        public List<V_VehicleData> VehicleData()
        {
            return CUltils.db.V_VehicleData.ToList();
        }
        public void upDate(Vehicle vehicle)
        {
            CUltils.db.SaveChanges();
        }
        public void remove(Vehicle vehicle)
        {
            CUltils.db.Vehicles.Remove(vehicle);
            CUltils.db.SaveChanges();
        }
        public void add(Vehicle vehicle)
        {
            CUltils.db.Vehicles.Add(vehicle);
            CUltils.db.SaveChanges();
        }
    }
}
