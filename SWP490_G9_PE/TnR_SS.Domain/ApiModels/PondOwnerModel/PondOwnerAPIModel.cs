using System;
using System.ComponentModel.DataAnnotations;

namespace TnR_SS.Domain.ApiModels.PondOwnerModel
{
    public class PondOwnerApiModel

    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        [RegularExpression(@"(84|0[3|5|7|8|9])+([0-9]{8})\b", ErrorMessage = "Phone Number invalid")]
        public string PhoneNumber { get; set; }

        public int TraderID{ get; set; }
    }
}
