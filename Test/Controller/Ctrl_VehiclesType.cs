using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controller
{
    internal class Ctrl_VehiclesType
    {
        public VehicleType GetVehiclesbyCategory(int ID)
        {
            return CUltils.db.VehicleTypes.FirstOrDefault(id => id.IDVehicleType == ID);    
        }
    }
}
