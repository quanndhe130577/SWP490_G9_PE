namespace TnR_SS.API.Areas.OTPManagement.Model.RequestModel
{
    public class StringeeReqModel
    {
        SMSContentReqModel SMS { get; set; }
    }

    public class SMSContentReqModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
    }
}
