using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.InterfaceQuerys
{
    public interface IUserQuery
    {
        public Task<User?> GetByID(int id);
        public Task<bool> EmailExist(string email);
    }
}
