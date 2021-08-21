using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.PurchaseModal
{
    public class ChotSoApiModel
    {
        public int ID { get; set; }

        [Range(0.0, 100.0, ErrorMessage = "Thuộc tính {0} phải thuộc khoảng [{1} - {2}]")]
        public double CommissionPercent { get; set; }

        [Required]
        public bool IsPaid { get; set; } = false;
        public double SentMoney { get; set; }
    }
}
