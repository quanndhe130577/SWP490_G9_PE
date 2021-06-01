using System;
using System.Security.Cryptography;
using System.Text;

namespace TnR_SS.API.Common.HandleSHA256
{
    public static class EncryptHandle
    {
        public static string EncryptString(String word)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                //Chuyển kiểu chuổi thành kiểu byte
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(word);
                //mã hóa chuỗi đã chuyển
                byte[] hash = sha256.ComputeHash(inputBytes);
                //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
