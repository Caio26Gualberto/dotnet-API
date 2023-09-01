using dotnet_API.Dtos;
using dotnet_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateUserDto input)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterAccountAsync(input);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest(new {Message = "Alguma propriedade não é valida"});
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto input)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(input);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            return BadRequest(new {Message = "Alguma propriedade não é valida"});
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _userService.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(new { Message = "Alguma propriedade não é valida" });
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassoword(string email)
        {
            if(string.IsNullOrEmpty(email))
                return NotFound();

            var result = await _userService.ForgetPasswordAsync(email);

           if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(UpdatePasswordDto input)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(input);

                if (result.IsSuccess) 
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest(new { Message = "Alguma propriedade não é valida" });
        }
    }
}
