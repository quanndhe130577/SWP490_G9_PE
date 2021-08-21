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
        public int PondOwnerId { get; set; }

        public double TotalWeight { get; set; }

        public double TotalAmount { get; set; }

        public string Status { get; set; } // đã chốt sổ hay chưa Completed/Pending

        public double PayForPondOwner { get; set; }

        public double Commission { get; set; }
        public bool isPaid { get; set; } = false;
        public double SentMoney { get; set; }
        /* public List<FishTypeApiModel> ListFishTypeWithPrice { get; set; }

         public PurchaseResModel()
         {
             this.ListFishTypeWithPrice = new List<FishTypeApiModel>();
         }*/
    }
}
