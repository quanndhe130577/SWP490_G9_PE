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

        public double Status { get; set; }
        public double Bonus { get; set; }
        public double Punish { get; set; }

        public double? Salary { get; set; }

        public double? BaseSalary { get; set; }

        public double? AdvanceSalary { get; set; }

        public bool Leaved { get; set; }

    }

}
