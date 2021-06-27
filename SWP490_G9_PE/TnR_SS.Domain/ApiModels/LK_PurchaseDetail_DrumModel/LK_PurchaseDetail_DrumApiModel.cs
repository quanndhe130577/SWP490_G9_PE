using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.LK_PurchaseDetail_DrumModel
{
    public class LK_PurchaseDetail_DrumApiModel
    {
        [Required]
        public int DrumID { get; set; }
        public int TruckID { get; set; }
        /*[Required]
        public double Weight { get; set; }*/

    }
}
