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

        public Vehicle GetVehicleByLicensePlate(string licensePlate)
        {
            
                var vehicle = CUltils.db.Vehicles
                    .FirstOrDefault(v => v.LicensePlate == licensePlate);
                return vehicle;
        }



        public List<object> GetAvailableVehicles()
        {
            var veh = CUltils.db.Vehicles.Include("VehicleType").Select(v => new
            {
                v.LicensePlate,
                VehicleTypeName = v.VehicleType.VehicleTypeName,
                v.Color,
                v.Status,
                v.price,
                Manufacture = v.VehicleType.Manufacturer,
                ManufactureYear = v.VehicleType.ManufactureYear,
                v.Description,
                v.IDEmployee,

            }).OrderBy(v => v.price)
                .ToList();

            return veh.Cast<object>().ToList();
        }

        public List<object> getList()
        {
            var veh = CUltils.db.Vehicles.Include("VehicleType").Select(v => new
            {
                v.LicensePlate,
                VehicleTypeName = v.VehicleType.VehicleTypeName,
                v.Color,
                v.Status,
                v.price,
                Manufacture = v.VehicleType.Manufacturer,
                ManufactureYear = v.VehicleType.ManufactureYear,
                v.Description,
                v.IDEmployee,
                
            }).OrderBy(v => v.price)
                .ToList();

            return veh.Cast<object>().ToList();
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

        public List<V_VehicleData> VehicleData()
        {
            using (var context = new BTXEntities1() ) {
                var data = context.V_VehicleData.ToList();
                return data;
            }
        }
        public List<V_VehicleData> priceIncrese()
        {
            return VehicleData().OrderBy(v => v.price).ToList();
        }
        public List<V_VehicleData> priceDerese()
        {
            return VehicleData().OrderByDescending(v => v.price).ToList();
        }
    }
}
