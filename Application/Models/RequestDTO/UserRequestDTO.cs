﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.RequestDTO
{
    public class UserRequestDTO
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime dateBirth { get; set; }
    }
}
