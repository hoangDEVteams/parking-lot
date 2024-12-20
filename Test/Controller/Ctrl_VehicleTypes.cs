﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controller
{
    internal class Ctrl_VehicleTypes
    {
        public VehicleType GetVehiclesbyCategory(int ID)
        {
            return CUltils.db.VehicleTypes.FirstOrDefault(id => id.IDVehicleType == ID);
        }
        public void AddVehicleType(VehicleType vehicletype)
        {
            CUltils.db.VehicleTypes.Add(vehicletype);
            CUltils.db.SaveChanges();
        }
    }
}
