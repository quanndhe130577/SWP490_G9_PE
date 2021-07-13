using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public class Truck : BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string LicensePlate { get; set; }

        public string Name { get; set; }


        [Required]
        public int TraderID { get; set; }
        public UserInfor Trader { get; set; }

        public List<Drum> Drums { get; set; }
    }
}
