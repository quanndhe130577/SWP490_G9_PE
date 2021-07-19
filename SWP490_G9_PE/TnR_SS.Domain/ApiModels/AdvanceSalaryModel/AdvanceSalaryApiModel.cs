using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.AdvanceSalaryModel
{
    public class AdvanceSalaryApiModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }

        public double Debt { get; set; }

        public int EmpId { get; set; }


        public bool Paid { get; set; }
    }
}
