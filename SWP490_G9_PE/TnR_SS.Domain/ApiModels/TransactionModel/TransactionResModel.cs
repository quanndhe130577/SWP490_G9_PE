using System;
using System.ComponentModel.DataAnnotations;

namespace TnR_SS.Domain.ApiModels.TransactionModel
{
    public class TransactionResModel
    {
        public int ID { get; set; }
        public TraderInformation Trader { get; set; }
        public DateTime Date { get; set; }
    }

    public class TraderInformation
    {
        public int ID { get; set; }

        [MaxLength(12)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Lastname { get; set; }
    }
}
