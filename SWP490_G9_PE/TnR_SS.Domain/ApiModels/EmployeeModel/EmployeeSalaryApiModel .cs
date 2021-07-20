using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.EmployeeModel
{
    public class EmployeeSalaryApiModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public double? Slalry { get; set; }
        public double AdvanceSlalry { get; set; }
    }

}
