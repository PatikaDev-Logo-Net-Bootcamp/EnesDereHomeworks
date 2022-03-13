using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto userRegisterDto)
        {
            return Ok("kayıt edildi.");
        }
        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            return Ok("login yapıldı.");
        }
    }
}
