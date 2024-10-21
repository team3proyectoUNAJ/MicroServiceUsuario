using Application.Interface.InterfaceQuerys;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class UserQuery : IUserQuery
    {
        private readonly MicroServiceUserContext _context;

        public UserQuery(MicroServiceUserContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByID(int id)
        {
            User? user = await _context.User.FirstOrDefaultAsync(i => i.UserID == id);
            return user;
        }

        public async Task<bool> EmailExist(string email)
        {
            return await _context.User.AnyAsync(e => e.Email == email);
        }
    }
}
