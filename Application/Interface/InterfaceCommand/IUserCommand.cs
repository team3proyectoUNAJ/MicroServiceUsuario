using Application.Models.RequestDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.InterfaceCommand
{
    public interface IUserCommand
    {
        public Task<User> InsertUser(User user);
        public Task<User> UpdateUser(UpdateUserRequestDTO userRequestDTO, int id);
        public Task DeleteUser(int id);
    }
}
