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
        [Required]
        public int ID { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public float Weight { get; set; }
    }
}
