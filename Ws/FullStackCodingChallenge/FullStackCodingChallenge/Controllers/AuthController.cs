using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullStackCodingChallenge.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        //private readonly IUserService _userService;

        //public AuthController(IUserService userService)
        //{
        //    _userService = userService;
        //}

        //[HttpPost("register")]
        //public async Task<IActionResult> Register(UserRegisterDto dto)
        //{
        //    var result = await _userService.RegisterAsync(dto);
        //    return result.Success ? Ok(result) : BadRequest(result);
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login(UserLoginDto dto)
        //{
        //    var token = await _userService.LoginAsync(dto);
        //    return string.IsNullOrEmpty(token) ? Unauthorized() : Ok(new { Token = token });
        //}

        //[Authorize]
        //[HttpGet("protected")]
        //public IActionResult ProtectedEndpoint()
        //{
        //    return Ok(new { Message = "This is a protected endpoint" });
        //}
    }
}
