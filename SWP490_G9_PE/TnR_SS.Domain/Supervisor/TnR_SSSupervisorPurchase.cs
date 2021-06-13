using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.PurchaseModal;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<int> CreatePurchaseAsync(PurchaseApiModel purchaseModel)
        {
            var purchase = _mapper.Map<PurchaseApiModel, Purchase>(purchaseModel);
            await _purchaseRepository.CreateAsync(purchase);
            return purchase.ID;
        }
    }
}
