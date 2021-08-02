using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.HistorySalaryEmpModel
{
    public class CreateHistorySalaryEmpModel
    {
        public DateTime DateStart { get; set; }
        public double Salary { get; set; }

        public int EmpId { get; set; }
    }
}
