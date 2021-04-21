using System.Security.Cryptography;
using System.Text;

namespace WhoAreYou_Xamarin.Services
{
    class EncryptoService
    {
        /// <summary>
        /// sha 256 암호화
        /// </summary>
        public string Generate(string value)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(value));

            StringBuilder sb = new StringBuilder();
            
            foreach(var i in hash)
            {
                sb.AppendFormat("{0:x2}", i);
            }

            return sb.ToString();
        }
    }
}
