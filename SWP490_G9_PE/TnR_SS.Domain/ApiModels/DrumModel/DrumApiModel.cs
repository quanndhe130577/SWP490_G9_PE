using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.DrumModel
{
    public class DrumApiModel
    {
        public int ID { get; set; }

        public int TruckId { get; set; }
        [MaxLength(20, ErrorMessage = "Tên lồ không thể dài hơn 20 ký tự")]

        public string Number { get; set; }
        [MaxLength(50, ErrorMessage = "Loại của lồ không thể dài hơn 50 ký tự")]
        public string Type { get; set; }

        public double MaxWeight { get; set; }
    }
}
