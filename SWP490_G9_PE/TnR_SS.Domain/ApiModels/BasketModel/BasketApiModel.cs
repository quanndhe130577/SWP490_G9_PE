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
        [Required(ErrorMessage = "Chưa xác định loại của rổ")]
        public string Type { get; set; }
        [Required]
        [RegularExpression(@"^[1-9]\d*(\.\d+)?$", ErrorMessage = "Khối lượng không chính xác")]
        public double Weight { get; set; }
    }
}
