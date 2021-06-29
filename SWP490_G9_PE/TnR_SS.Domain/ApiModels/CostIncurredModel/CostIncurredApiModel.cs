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

        [Required]
        public double Cost { get; set; }

        public string Note { get; set; }
        
        public DateTime TimeCreate { get; set; }
    }
}
