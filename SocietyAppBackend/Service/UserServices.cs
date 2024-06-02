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
            try
            {
                var users = _dbcontext.UserTable.ToList();
                var mapperUsers = _mapper.Map<List<UserViewDto>>(users);
                return mapperUsers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserViewByIdFollow> GetUserById(int id)
        {
            try
            {
                var user = await _dbcontext.UserTable.FirstOrDefaultAsync(u => u.UserId == id);
                var userTotalView = new UserViewByIdFollow { UserId = id, Email = user.Email, Username = user.Username, PasswordHash = user.PasswordHash, ProfilePictureUrl = user.ProfilePictureUrl, Role = user.Role, Bio = user.Bio, IsBlocked = user.IsBlocked, IsActive = user.IsActive, Posts = await _dbcontext.Posts.Where(i => i.UserId == id).CountAsync(), Followings = await _dbcontext.Follows.Where(i => i.FollowerId == id).CountAsync(), Followers = await _dbcontext.Follows.Where(i => i.FollowingId == id).CountAsync() };
                return userTotalView;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> RegisterUser(UserDto userdto,IFormFile image) {
            try
            {
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
                _dbcontext.UserTable.Add(new User { Username = userdto.Username, Email = userdto.Email, PasswordHash = userdto.PasswordHash, ProfilePictureUrl = profilePic, });
                await _dbcontext.SaveChangesAsync();
                return "registered successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<User> Login(LoginDto login)
        {
            try
            {
                var existingUser = await _dbcontext.UserTable.FirstOrDefaultAsync(i => i.Username == login.Username);
                return existingUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
            catch (Exception ex)
            {
                throw new Exception("Error adding product:" + ex.Message);
            }

        }
    }
}
