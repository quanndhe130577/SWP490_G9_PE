using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.FishTypeModel
{
    public class FishTypeWithPriceResModel
    {
        [Required]
        public int FTID { get; set; }
        [Required]
        public string FishName { get; set; }
        public string Description { get; set; }
        [Required]
        public float MinWeight { get; set; }
        [Required]
        public float MaxWeight { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
