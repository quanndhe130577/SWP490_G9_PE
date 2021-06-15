using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TnR_SS.Domain.ApiModels.FishTypeModel;

namespace TnR_SS.Domain.ApiModels.PurchaseModal
{
    public class PurchaseResModel
    {
        public int ID { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string PondOwnerName { get; set; }


        public List<FishTypeWithPriceResModel> ListFishTypeWithPrice { get; set; }
    }
}
