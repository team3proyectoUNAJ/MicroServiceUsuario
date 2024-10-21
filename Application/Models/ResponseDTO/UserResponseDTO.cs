using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ResponseDTO
{
    public class UserResponseDTO
    {
        public int userID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public byte[] photo { get; set; }
        public DateTime dateBirth { get; set; }
    }
}
