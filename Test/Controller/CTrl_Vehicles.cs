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
        public List<Vehicle> getList()
        {
            return CUltils.db.Vehicles.Include("VehicleType").ToList();
        }

        public void upDate(Vehicle loaiSach)
        {
            CUltils.db.SaveChanges();
        }
        public void remove(Vehicle vehicle)
        {
            CUltils.db.Vehicles.Remove(vehicle);
            CUltils.db.SaveChanges();
        }
        public void addNewVehicle(Vehicle vehicle)
        {
            CUltils.db.Vehicles.Add(vehicle);
        }
        public void add(Vehicle VC)
        {
            if (VC.VehicleType != null)
            {
                var existingVehicleType = CUltils.db.VehicleTypes
                    .FirstOrDefault(vt => vt.VehicleTypeName == VC.VehicleType.VehicleTypeName &&
                                           vt.Manufacturer == VC.VehicleType.Manufacturer &&
                                           vt.ManufactureYear == VC.VehicleType.ManufactureYear);

                if (existingVehicleType == null)
                {
                    CUltils.db.VehicleTypes.Add(VC.VehicleType);
                    CUltils.db.SaveChanges();
                }
                else
                {
                    VC.VehicleType = existingVehicleType;
                }
            }
            CUltils.db.Vehicles.Add(VC);
            CUltils.db.SaveChanges();
        }
        public Vehicle GetVehicleByID(int id)
        {
            return CUltils.db.Vehicles.FirstOrDefault(v => v.IDVehicleType == id);
        }
    }
}
