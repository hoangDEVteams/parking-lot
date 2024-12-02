using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controller
{
    internal class Ctrl_PenaltiesDetail
    {
        public PenaltyDetail getInforPenaty(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return CUltils.db.PenaltyDetails
           .FirstOrDefault(p => p.IDPenalty == id);
        }
    }
}
