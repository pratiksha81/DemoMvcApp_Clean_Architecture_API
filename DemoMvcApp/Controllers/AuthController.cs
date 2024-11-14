using DemoMvcApp.DTOs;
using DemoMvcApp.Models;
using DemoMvcApp.Infrastructure;
using DemoMvcApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoMvcApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AuthController(IUserRepository userRepository, JwtTokenHelper jwtTokenHelper)
        {
            _userRepository = userRepository;
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] loginDto loginDto)
        {
            var user = _userRepository.Authenticate(loginDto.Username, loginDto.Password);
            if (user == null) return Unauthorized();

            var token = _jwtTokenHelper.GenerateToken(user);
            return Ok(new { token });
        }

        
    }
    
}
