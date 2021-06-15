using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.FishTypePriceModel;

namespace TnR_SS.Domain.ApiModels.PurchaseModal
{
    public class PurchaseApiModel
    {
        public int ID { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid PondOwnerID { get; set; }

        [Required]
        public int TraderID { get; set; }

        public List<FishTypeWithPriceApiModel> ListFishTypeWithPrice { get; set; }
    }
}
