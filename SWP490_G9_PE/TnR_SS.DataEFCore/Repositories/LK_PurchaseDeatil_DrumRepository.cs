using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.DrumModel;
using TnR_SS.Domain.ApiModels.LK_PurchaseDetail_DrumModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class LK_PurchaseDeatil_DrumRepository : RepositoryBase<LK_PurchaseDeatil_Drum>, ILK_PurchaseDeatil_DrumRepository
    {
        public LK_PurchaseDeatil_DrumRepository(TnR_SSContext context) : base(context) { }

        
    }
}
