using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.TimeKeepingModel
{
    public class TimeKeepingApiModel
    {
        public int ID { get; set; }
        [Required]
        public int WorkDay { get; set; }
        [Required]
        public string Status { get; set; }
        public double Money { get; set; }
        public string Note { get; set; }
        [Required]
        public int EmpId { get; set; }
    }
}
