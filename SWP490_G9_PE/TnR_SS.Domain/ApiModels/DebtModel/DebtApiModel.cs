using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.DebtModel
{
    public class DebtApiModel
    {
        public string Creditors { get; set; }

        public string Debtor { get; set; }

        public double DebtMoney { get; set; }

        public DateTime Date { get; set; }

        public bool isPaid { get; set; }
    }
}
