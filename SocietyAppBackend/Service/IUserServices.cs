using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocietyAppBackend.Data;
using SocietyAppBackend.ModelEntity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocietyAppBackend.Service
{
    public interface IUserServices
    {

        Task<string> RegisterUser(UserDto userdto);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> Login(LoginDto login);



    }
   
}
