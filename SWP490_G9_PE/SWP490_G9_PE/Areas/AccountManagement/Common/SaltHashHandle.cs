using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TnR_SS.API.Areas.AccountManagement.Common
{
    public static class SaltHashHandle
    {
        public static string RandomSaltHash()
        {
            string rs = "";
            Random rd = new Random();
            for (int i = 0; i < 20; i++)
            {
                rs += Convert.ToString((Char)rd.Next(65, 90));
            }
            return rs;
        }
    }
}
