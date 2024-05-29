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

        Task<string> RegisterUser(UserDto userdto, IFormFile image);
         Task<List<UserViewDto>> GetAllUsers();
        Task<UserViewByIdFollow> GetUserById(int id);
        Task<User> Login(LoginDto login);
         Task<string> BlockUser(int userid);
        Task<string> UnBlockUser(int userid);
        Task UpdateUserData(int userid, UpdateUserDto user, IFormFile image);
        Task<string> DeleteRegisteredUser(int id);






    }

}
