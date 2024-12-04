using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Views;

namespace Test.Controller
{
    internal class Ctrl_Employees
    {
        public string taoIDNV()
        {
            var ID = CUltils.db.Employees.Select(a => a.IDEmployee).OrderByDescending(id => id).FirstOrDefault();
            if (string.IsNullOrEmpty(ID))
            {
                return "NV001";
            }
            string check = ID.StartsWith("NV") ? ID.Substring(2) : ID;
            int n;
            if (int.TryParse(check, out n))
            {
                n++;
                return "NV" + n.ToString("D3");
            }
            throw new Exception("ID nhân viên không hợp lệ");

        }
        public List<Employee> findAll()
        {
            return CUltils.db.Employees.ToList();

        }
        public void add(Employee employee)
        {
            employee.IDEmployee = taoIDNV();
            CUltils.db.Employees.Add(employee);
            CUltils.db.SaveChanges();
        }
       
    }
}
