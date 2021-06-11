using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public class PondOwner
    {
        [Key]
        [Required]
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public PondOwner()
        {
            this.ID = Guid.NewGuid();
        }

        public List<TongKetMua> TongKetMuas { get; set; }
    }
}
