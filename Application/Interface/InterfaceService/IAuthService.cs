using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.InterfaceService
{
    public interface IAuthService
    {
        byte[] GenerateSalt();
        byte[] HashPassword(string password, byte[] salt);
    }
}
