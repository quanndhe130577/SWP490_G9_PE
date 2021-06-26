using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.LK_PurchaseDetail_DrumModel;

namespace TnR_SS.Domain.ApiModels.PurchaseDetailModel
{
    public class PurchaseDetailReqModel
    {

        [Required]
        public int FishTypeID { get; set; }

        [Required]
        public int BasketId { get; set; }

        [Required]
        public int PurchaseId { get; set; }

        [Required]
        public List<LK_PurchaseDetail_DrumApiModel> ListDrum { get; set; }
    }
}