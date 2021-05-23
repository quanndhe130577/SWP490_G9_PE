using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.HandleSHA256;

namespace TnR_SS.API.Authentication.Common
{
    public static class LoginHandle
    {
        public static bool CheckPassword(string encryptPass, string pass, string saltHash)
        {
            if (encryptPass.Equals(HandleSHA256.EncryptString(pass + saltHash)))
            {
                return true;
            }
            return false;
        }
    }
}
