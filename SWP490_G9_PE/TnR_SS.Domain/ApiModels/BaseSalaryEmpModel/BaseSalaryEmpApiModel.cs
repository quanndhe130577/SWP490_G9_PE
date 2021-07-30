using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.HistorySalaryEmpModel
{
    public class BaseSalaryEmpApiModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Salary { get; set; }
        public int EmpId { get; set; }
        public string EmpName { get; set; }
    }
}
