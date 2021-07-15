﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int TraderId { get; set; }
        public UserInfor Trader { get; set; }

        /*[Required]
        public int BuyerId { get; set; }*/
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int WeightRecorderId { get; set; }
        public UserInfor WeightRecorder { get; set; }

        [Required]
        public double CommissionUnit { get; set; }
        public List<TransactionDetail> TransactionDetails { get; set; }


    }
}
