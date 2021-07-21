using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.ApiModels.TimeKeepingModel
{
    public class TimeKeepingApiModel
    {
        public int ID { get; set; }
        public DateTime WorkDay { get; set; }
        public double Status { get; set; }
        public TimeKeepingNote Note { get; set; }
        public string EmpName { get; set; }
        public int EmpId { get; set; }
    }
}
