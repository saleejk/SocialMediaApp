using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocietyAppBackend.ModelEntity;
using SocietyAppBackend.Service;

namespace SocietyAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public readonly IRegister _register;
        public RegisterController(IRegister register)
        {
            _register = register;
        }
        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(UserDto userDto)
        {
            _register.RegisterUser(userDto);
            return Ok("Register Completed");
        }
        [HttpGet("GetAllUser")]
        public IActionResult GetAllUsers() {
            return Ok(_register.GetAllUsers());
        }
    }
}
