using Application.Models.RequestDTO;
using Application.Models.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.InterfaceService
{
    public interface IUserService
    {
        public Task<UserResponseDTO> CreateUser(UserRequestDTO user);
        public Task DeleteUser(int id);
        public Task<UserResponseDTO> GetById(int id);
        public Task<UserResponseDTO> UpdateUser(UpdateUserRequestDTO user, int id);

    }
}
