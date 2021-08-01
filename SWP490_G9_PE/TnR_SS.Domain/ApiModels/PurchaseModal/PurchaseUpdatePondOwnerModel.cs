using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.PurchaseModal
{
    public class PurchaseUpdatePondOwnerModel
    {
        public int PurchaseId { get; set; }
        public int PondOwnerId { get; set; }
    }
}
