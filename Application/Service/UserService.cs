using Application.Exceptions;
using Application.Interface.InterfaceCommand;
using Application.Interface.InterfaceQuerys;
using Application.Interface.InterfaceService;
using Application.Models.RequestDTO;
using Application.Models.ResponseDTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserCommand _command;
        private readonly IUserQuery _query;
        private readonly IAuthService _authService;

        public UserService(IUserCommand command, IUserQuery query, IAuthService authService)
        {
            _command = command;
            _query = query;
            _authService = authService;
        }

        public async Task<UserResponseDTO> CreateUser(UserRequestDTO user)
        {
            if (await _query.EmailExist(user.email))
                throw new DuplicateEntityException("The email of the user already exists.");
            if ((DateTime.Now.Year) - (user.dateBirth.Year) <= 18)
                throw new ArgumentOutOfRangeException("The user must be of legal age.");
            var salt = _authService.GenerateSalt();
            var hash = _authService.HashPassword(user.password, salt);
            User myUser = new User
            {
                Name = user.name,
                Email = user.email,
                Password = hash,
                PasswordSalt = salt,
                Type = 2,
                Photo = null,
                DateBirth = user.dateBirth,
                CreateDate = DateTime.Now,
            };
            User newUser = await _command.InsertUser(myUser);
            return new UserResponseDTO
            {
                userID = newUser.UserID,
                name = newUser.Name,
                email = newUser.Email,
                photo = newUser.Photo,
                dateBirth = newUser.DateBirth
            };
        }

        public async Task DeleteUser(int id)
        {
            await _command.DeleteUser(id);
        }

        public async Task<UserResponseDTO> GetById(int id)
        {
            User user = await _query.GetByID(id);
            if (user == null)
                throw new NotFoundException("No user found with the entered id");
            return new UserResponseDTO
            {
                userID = user.UserID,
                name = user.Name,
                email = user.Email,
                photo = user.Photo,
                dateBirth = user.DateBirth
            };
        }

        public async Task<UserResponseDTO> UpdateUser(UpdateUserRequestDTO user, int id)
        {
            User exist = await _query.GetByID(id);
            if (exist == null)
                throw new NotFoundException("No user found with the entered id");
            if (await _query.EmailExist(user.email))
                throw new DuplicateEntityException("The email of the user already exists.");
            User myUser = await _command.UpdateUser(user, id);
            return new UserResponseDTO
            {
                userID = myUser.UserID,
                name = myUser.Name,
                email = myUser.Email,
                photo = myUser.Photo,
                dateBirth = myUser.DateBirth
            };
        }
        public byte[] GenerateSalt()
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                return hmac.Key;
            }
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
