using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TnR_SS.Domain.ApiModels.FishTypeModel;

namespace TnR_SS.Domain.ApiModels.PurchaseModal
{
    public class PurchaseCreateReqModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int PondOwnerID { get; set; }

        [Required]
        public int TraderID { get; set; }

    }
}
