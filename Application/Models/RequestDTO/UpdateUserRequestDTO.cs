using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.RequestDTO
{
    public class UpdateUserRequestDTO
    {
        public string? name { get; set; }
        public string? email { get; set; }
        public byte[]? photo { get; set; }
    }
}
