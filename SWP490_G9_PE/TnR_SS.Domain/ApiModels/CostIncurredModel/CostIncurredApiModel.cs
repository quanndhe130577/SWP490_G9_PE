using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.CostIncurredModel
{
    public class CostIncurredApiModel
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [RegularExpression(@"^[1-9]\d*(\.\d+)?$", ErrorMessage = "Cost invalid")]
        [Required]
        public double Cost { get; set; }

        public string Note { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
