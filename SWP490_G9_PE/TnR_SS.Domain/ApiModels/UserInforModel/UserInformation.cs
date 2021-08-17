using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.UserInforModel
{
    public class UserInformation : IComparable
    {
        public int ID { get; set; }

        [MaxLength(12)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int CompareTo(object obj)
        {
            var newObj = (UserInformation)obj;

            return this.LastName.CompareTo(newObj.LastName);
        }
    }
}
