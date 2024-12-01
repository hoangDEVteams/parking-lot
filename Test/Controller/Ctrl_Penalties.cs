using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controller
{
    internal class Ctrl_Penalties
    {
        public List<Penalty> GetPenaltiesByCustomerId(string customerId)
        {
            var penalties = CUltils.db.Penalties
                .Where(p => p.IDCustomer == customerId)
                .ToList();
            return penalties;
        }

        // Lấy chi tiết của một phiếu phạt theo IDPenalty
        public List<PenaltyDetail> GetPenaltyDetailsByPenaltyId(int penaltyId)
        {
            var penaltyDetails = CUltils.db.PenaltyDetails
                .Where(pd => pd.IDPenalty == penaltyId)
                .Select(pd => new
                {
                    IDPenaltyDetail = pd.IDPenaltyDetail,
                    IDPenalty = pd.IDPenalty,
                    Reason = pd.Reason,
                    price = pd.price
                })
                .ToList()
                .Select(pd => new PenaltyDetail
                {
                    IDPenaltyDetail = pd.IDPenaltyDetail,
                    IDPenalty = pd.IDPenalty,
                    Reason = pd.Reason,
                    price = pd.price
                })
                .ToList();

            return penaltyDetails;
        }
    }

}