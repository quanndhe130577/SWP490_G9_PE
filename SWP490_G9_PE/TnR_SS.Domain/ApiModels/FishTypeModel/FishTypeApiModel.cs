using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.FishTypeModel
{
    public class FishTypeApiModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string FishName { get; set; }
        public string Description { get; set; }
        [Required]
        public float MinWeight { get; set; }
        [Required]
        public float MaxWeight { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public int? PurchaseID { get; set; }
        public double TransactionPrice { get; set; }
    }
}
