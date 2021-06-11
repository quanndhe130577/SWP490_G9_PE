using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public class Purchase
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        [Required]
        public double TotalWeight { get; set; }

        [Required]
        public double PayForPondOwner { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public double TienGioiThieu { get; set; }

        [Required]
        public bool isPaid { get; set; }

        public double PondBackMoney { get; set; }

        [Required]
        public Guid PondOwnerID { get; set; }
        public PondOwner PondOwner { get; set; }

        [Required]
        public int TraderID { get; set; }
        public UserInfor UserInfor { get; set; }

        public List<PurchaseDetail> TransactionBuys { get; set; }
    }
}
