using Application.Interface.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class AuthService : IAuthService
    {
        public byte[] GenerateSalt()
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                return hmac.Key; // Genera el salt
            }
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
