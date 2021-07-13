using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public class Buyer : BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        [MaxLength(11)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }

        public int WeightRecorderId { get; set; }

        public UserInfor WeightRecorder { get; set; }
        public List<TransactionDetail> TransactionDetails { get; set; }
    }
}
