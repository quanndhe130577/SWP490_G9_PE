using System;
namespace TnR_SS.Domain.ApiModels.PondOwnerModel
{
    public class PondOwnerAPIModel

    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public int TraderID{ get; set; }
    }
}
