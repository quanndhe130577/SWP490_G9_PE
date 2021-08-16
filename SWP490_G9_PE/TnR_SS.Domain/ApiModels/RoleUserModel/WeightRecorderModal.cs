namespace TnR_SS.Domain.ApiModels.RoleUserModel
{
    public class WeightRecorderModal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int WrId { get; set; }
        public int TraderId { get; set; }
        public bool CanDelete { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDeleted { get; set; }
    }
}
