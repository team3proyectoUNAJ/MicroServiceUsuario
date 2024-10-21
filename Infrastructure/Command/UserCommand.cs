using Application.Exceptions;
using Application.Interface.InterfaceCommand;
using Application.Models.RequestDTO;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Command
{
    public class UserCommand : IUserCommand
    {
        private readonly MicroServiceUserContext _context;

        public UserCommand(MicroServiceUserContext context)
        {
            _context = context;
        }

        public async Task DeleteUser(int id)
        {
            User user = await _context.User.FirstOrDefaultAsync(i => i.UserID == id);

            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException("No user found with the entered id");
        }

        public async Task<User> InsertUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(UpdateUserRequestDTO userRequestDTO, int id)
        {
            User user = await _context.User.FirstOrDefaultAsync(i => i.UserID == id);
            if (!string.IsNullOrEmpty(userRequestDTO.name) && userRequestDTO.name != "string")
                user.Name = userRequestDTO.name;
            if (!string.IsNullOrEmpty(userRequestDTO.email) && userRequestDTO.email != "string")
                user.Email = userRequestDTO.email;
            if (userRequestDTO.photo != null)
                user.Photo = userRequestDTO.photo;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
