using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Identity.Client;
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
        public readonly IWebHostEnvironment _webHostEnvironment;
        public IUserService(DbContextClass dbcontext, IMapper mapper, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;

            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<UserViewDto>> GetAllUsers()
        {
            var users= _dbcontext.UserTable.ToList();
            var mapperUsers = _mapper.Map<List<UserViewDto>>(users);
            return mapperUsers;
        }
        public async Task<UserViewByIdFollow> GetUserById(int id)
        {
            var user=await _dbcontext.UserTable.FirstOrDefaultAsync(u => u.UserId == id);
            var userTotalView = new UserViewByIdFollow { UserId = id, Email = user.Email, Username = user.Username, PasswordHash = user.PasswordHash, ProfilePictureUrl = user.ProfilePictureUrl, Role = user.Role, Bio = user.Bio, IsBlocked = user.IsBlocked, IsActive = user.IsActive, Posts = await _dbcontext.Posts.Where(i => i.UserId == id).CountAsync(), Followings = await _dbcontext.Follows.Where(i => i.FollowerId == id).CountAsync(), Followers = await _dbcontext.Follows.Where(i => i.FollowingId == id).CountAsync() };
            return userTotalView;
        }

        public async Task<string> RegisterUser(UserDto userdto,IFormFile image) {
            string profilePic = null;
            if (image != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "Post", filename);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);

                }
                profilePic = "/Uploads/Post" + filename;
            }
            else
            {
                profilePic = "Uploads/Common/IMG_4741.jpeg";
            }

            var isUserExist = await _dbcontext.UserTable.FirstOrDefaultAsync(i => i.Email == userdto.Email);
            if (isUserExist != null)
            {
                return "user Already Exist !";
            }
          
           

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(userdto.PasswordHash, salt);
            userdto.PasswordHash = hashPassword;
            _dbcontext.UserTable.Add(new User { Username = userdto.Username, Email = userdto.Email, PasswordHash = userdto.PasswordHash,ProfilePictureUrl= profilePic,});
            await _dbcontext.SaveChangesAsync();
            return "registered successfully";
        }
        public async Task<User> Login(LoginDto login)
        {
            var existingUser = await _dbcontext.UserTable.FirstOrDefaultAsync(i => i.Username == login.Username);
            return existingUser;
        }
        public async Task<string> BlockUser(int userid)
        {
            try
            {
                var userr = await _dbcontext.UserTable.FirstOrDefaultAsync(i => i.UserId == userid);
                if (userr == null)
                {
                    return "invalid user id";
                }
                userr.IsBlocked = true;
                await _dbcontext.SaveChangesAsync();
                return "blocked user successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> UnBlockUser(int userid)
        {
                try
                {
                    var userr = await _dbcontext.UserTable.FirstOrDefaultAsync(i => i.UserId == userid);
                    if (userr == null)
                    {
                        return "invalid user Id";
                    }
                    userr.IsBlocked = false;
                    await _dbcontext.SaveChangesAsync();
                    return "Unblocked user successfully";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
        }
        public async Task UpdateUserData(int userid, UpdateUserDto user, IFormFile image)
        {
            try
            {
                string profileImage = null;
                if (image != null && image.Length > 0)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "Post", filename);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    profileImage = "/Uploads/post/" + filename;
                }
                else
                {
                    profileImage = "Uploads/Common/IMG_4741.jpeg";

                }
                var updateUser = await _dbcontext.UserTable.FirstOrDefaultAsync(u => u.UserId == userid);
                updateUser.Email = user.Email;
                updateUser.Username = user.Username;
                updateUser.Bio = user.Bio;
                updateUser.ProfilePictureUrl = profileImage;
                await _dbcontext.SaveChangesAsync();



            }
            catch (Exception ex) {
                throw new Exception("Error adding product:" + ex.Message);
            }

        }
        public async Task<string> DeleteRegisteredUser(int id)
        {
            var user = await _dbcontext.UserTable.FirstOrDefaultAsync(i => i.UserId == id);
            if (user == null)
            {
                return "invalid userId";
            }
            _dbcontext.UserTable.Remove(user);
            await _dbcontext.SaveChangesAsync();
            return "user delete successfully";
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
