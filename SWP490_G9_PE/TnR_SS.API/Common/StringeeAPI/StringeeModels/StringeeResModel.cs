using System.Collections.Generic;

namespace TnR_SS.API.Common.StringeeAPI
{
    public class StringeeResModel
    {
        public string SMSSent { get; set; }
        public List<SMSResModel> result { get; set; }
    }
    public class SMSResModel
    {
        public string Price { get; set; }
        public string SMSType { get; set; }
        public string R { get; set; }
        public string MSG { get; set; }
    }
}
