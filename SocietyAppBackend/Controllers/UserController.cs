using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocietyAppBackend.ModelEntity;
using SocietyAppBackend.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocietyAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserServices _userService;
        public readonly IConfiguration _config;

        public UserController(IUserServices register, IConfiguration config)
        {
            _userService = register;
            _config = config;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromForm] UserDto userDto,IFormFile image)
        {
            try
            {
                var isExist = await _userService.RegisterUser(userDto,image);
                return Ok(isExist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"an error occured,{ex.Message}");
            }
        }
        [HttpGet("GetAllUser")]
        [Authorize]

        public async  Task<IActionResult> GetAllUsers()
        {
            return Ok( await _userService.GetAllUsers());
        }
        [HttpGet("GetUserById")]
        [Authorize]

        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                var existingUser = await _userService.Login(login);
                if (existingUser == null)
                {
                    return NotFound("username or password incorrect");
                }
                if (existingUser.IsBlocked)
                {
                    return BadRequest("access denied");
                }
                bool validatePassword = BCrypt.Net.BCrypt.Verify(login.Password, existingUser.PasswordHash);
                if (!validatePassword)
                {
                    return BadRequest("password dont match");
                }
                string token = GenerateToken(existingUser);
                return Ok(new { Token = token, email = existingUser.Email, name = existingUser.Username });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("block-user")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult>BlockUser(int id)
        {
            if (id == null)
            {
                return BadRequest("invalid id");
            }
            return Ok(await _userService.BlockUser(id));

        }
        [HttpPut("Unblock-user")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult>UnBlockUser(int id)
        {
            return Ok( await _userService.UnBlockUser(id));
        }
        [HttpPut("updateUserData")]
        [Authorize]

        public async Task<IActionResult>UpdateUserData(int userid,[FromForm]UpdateUserDto userdto,IFormFile image)
        {
            if (userid==null)
            {
                BadRequest("invalid id or datas");     
            }
            await _userService.UpdateUserData(userid, userdto, image);
            return Ok("user Update successfully");

        }
        private string GenerateToken(User users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, users.UserId.ToString()),
            new Claim(ClaimTypes.Name, users.Username),
            new Claim(ClaimTypes.Role, users.Role),
            new Claim(ClaimTypes.Email, users.Email),
        };

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(1)

            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        [HttpDelete("deleteRegUser")]
       // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> deleteRegUser(int id)
        {
            if (id == null)
            {
                return BadRequest("invalid userid");

                
            }

            return Ok(await _userService.DeleteRegisteredUser(id));
        }

    }
}