using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class HashSHA256 : IHash
    {
        public string GetHash(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] hash = mySHA256.ComputeHash(bytes);

            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }

            return hashString;
        }
    }
}
