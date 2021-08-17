using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.TruckModel
{
    public class TruckApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Biển số xe chỉ có tối đa 10 kí tự")]
        public string LicensePlate { get; set; }
    }
}
