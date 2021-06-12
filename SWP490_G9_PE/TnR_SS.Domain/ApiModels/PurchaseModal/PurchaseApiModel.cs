using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.PurchaseModal
{
    public class PurchaseApiModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid PondOwnerID { get; set; }

        [Required]
        public int TraderID { get; set; }

    }
}
