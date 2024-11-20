using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Test.Controller
{
    internal class Ctrl_Vehivles
    {
        public List<Vehicle> findByName(string name)
        {
            return CUltils.db.Vehicles.ToList().Where(t => t.VehicleName.ToLower().Contains(name.ToLower())).ToList();
        }
        public List<Vehicle> fillterByColor(string color)
        {
            return CUltils.db.Vehicles.ToList().Where(t => t.Color.Contains(color)).ToList();
        }
        public List<Vehicle> fillterByStatus(string status)
        {
            return CUltils.db.Vehicles.ToList().Where(t => t.Status.Contains(status)).ToList();
        }
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
