using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; } 
        public byte[] PasswordSalt { get; set; }
        public int Type { get; set; }
        public byte[]? Photo { get; set; }
        public DateTime DateBirth { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
