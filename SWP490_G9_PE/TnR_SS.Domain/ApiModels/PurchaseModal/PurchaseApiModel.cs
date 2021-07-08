using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.ApiModels.PurchaseModal
{
    public class PurchaseApiModel
    {
        public int ID { get; set; }

        public double PayForPondOwner { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public double Commission { get; set; } // tiền trả cho môi giới

       /* [Required]
        public bool IsPaid { get; set; } = false;*/

        //public string Status { get; set; } = PurchaseStatus.Pending.ToString();

        public double PondBackMoney { get; set; }

        [Required]
        public int PondOwnerID { get; set; }

    }
}
