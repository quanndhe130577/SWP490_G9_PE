using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.EmployeeModel
{
    public class EmployeeSalaryDetailApiModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public double? Paid { get; set; }

        public double? NotPaid { get; set; }

        public double? Salary { get; set; }

    }

}
