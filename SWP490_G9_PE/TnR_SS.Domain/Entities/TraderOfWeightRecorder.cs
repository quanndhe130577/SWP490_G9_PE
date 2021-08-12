using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public class TraderOfWeightRecorder : BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int TraderId { get; set; }
        public UserInfor Trader { get; set; }
        public int WeightRecorderId { get; set; }
        public UserInfor WeightRecorder { get; set; }
        public bool IsAccepted { get; set; } = false;
    }
}
