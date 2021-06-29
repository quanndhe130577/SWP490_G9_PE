using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.BasketModel.ResponseModel
{
    public class BasketApiModel
    {
        public int ID { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [RegularExpression(@"^[1-9]\d*(\.\d+)?$", ErrorMessage = "Weight invalid")]
        public double Weight { get; set; }
    }
}
