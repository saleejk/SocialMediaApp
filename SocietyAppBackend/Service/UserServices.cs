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
    public class IUserService : IUserServices
    {
        public readonly DbContextClass _dbcontext;
        public readonly IMapper _mapper;
        public readonly IConfiguration _configuration;
        public IUserService(DbContextClass dbcontext, IMapper mapper, IConfiguration configuration)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;

            _configuration = configuration;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return _dbcontext.UserTable.ToList();
        }
        public async Task<User>GetUserById(int id)
        {
            return _dbcontext.UserTable.FirstOrDefault(u => u.UserId == id);
        }

       public async Task<string> RegisterUser(UserDto userdto) {
            var isUserExist = await _dbcontext.UserTable.FirstOrDefaultAsync(i => i.Email == userdto.Email);
            if (isUserExist != null)
            {
                return "user Already Exist !";
            }
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(userdto.PasswordHash, salt);
            userdto.PasswordHash = hashPassword;
            _dbcontext.UserTable.Add(new User { Username=userdto.Username,Email=userdto.Email,PasswordHash=userdto.PasswordHash});
            await _dbcontext.SaveChangesAsync();
            return "registered successfully";
        }
        public async Task<User>Login(LoginDto login)
        {
            var existingUser = await _dbcontext.UserTable.FirstOrDefaultAsync(i => i.Username == login.Username);
            return existingUser;
        }
        
       
        //public async Task<string> LoginUser(LoginDto user)
        //{
        //    try
        //    {
        //        var data = await _dbcontext.UserTable.FirstOrDefaultAsync(u => u.Username == user.Username);
        //        if (data != null)
        //        {
        //            var passsword = BCrypt.Net.BCrypt.Verify(user.Password, data.PasswordHash);
        //            if (passsword)
        //            {
        //                if (data.IsStatus == false)
        //                    return "blocked";
        //                var tokenHandler = new JwtSecurityTokenHandler();
        //                var key = _configuration["JwtConfig:Key"];
        //                var tokenDescriptor = new SecurityTokenDescriptor
        //                {
        //                    Subject = new ClaimsIdentity(new Claim[]
        //                    {
        //                    new Claim(ClaimTypes.NameIdentifier,data.UserId.ToString()),
        //                    new Claim(ClaimTypes.Name,data.Username),
        //                    new Claim(ClaimTypes.Role,data.Role),
        //                    }),
        //                    Expires = DateTime.UtcNow.AddMinutes(30),
        //                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
        //                };
        //                var token = tokenHandler.CreateToken(tokenDescriptor);
        //                var tokenString = tokenHandler.WriteToken(token);
        //                return tokenString;
        //            }
        //        }
        //        return "NotFound";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Internal Server Error{ex.Message}");
        //    }
        //}
        //public async Task<string> LoginUser(LoginDto logindto)
        //{
        //    try
        //    {
        //        var data = await _dbcontext.UserTable.FirstOrDefaultAsync(u => u.Username == logindto.Username);
        //        if (data != null)
        //        {
        //            var Password = BCrypt.Net.BCrypt.EnhancedVerify(logindto.Password, data.PasswordHash, HashType.SHA256);
        //            if (Password)
        //            {
        //                if (data.IsStatus == false)
        //                    return "blocked";
        //                var tokenHandler = new JwtSecurityTokenHandler();
        //                var key = _configuration["JwtConfig:Key"];
        //                var tokenDescriptor = new SecurityTokenDescriptor
        //                {
        //                    Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, data.UserId.ToString()), new Claim(ClaimTypes.Name, data.Username), new Claim(ClaimTypes.Role, data.Role), }),
        //                    Expires = DateTime.UtcNow.AddMinutes(30),
        //                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
        //                };
        //                var token = tokenHandler.CreateToken(tokenDescriptor);
        //                var tokenString = tokenHandler.WriteToken(token);
        //                return tokenString;


        //            }

        //        }
        //        return "NotFound";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Internal Server Error{ex.Message}");
        //    }
        //}




    }
}
