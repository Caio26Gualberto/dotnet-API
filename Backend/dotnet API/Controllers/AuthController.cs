using dotnet_API.Controllers.Dto;
using dotnet_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Register([FromBody] CreateUserDto input)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterAsync(input);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest(new { Message = "Alguma propriedade não é valida" });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto input)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(input);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            return BadRequest(new { Message = "Alguma propriedade não é valida" });
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassoword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound();

            var result = await _userService.ForgetPasswordAsync(email);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [AllowAnonymous]
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

        [Authorize]
        [HttpGet("ContinueToMainPage")]
        public async Task<IActionResult> ContinueToMainPage()
        {
            var result = await _userService.ContinueToMainPageAsync();

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(new { Message = "Alguma propriedade não é valida" });
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var result = await _userService.RefreshTokenAsync((int)UserContext.UserId);

            if (!string.IsNullOrEmpty(result))
                return Ok(result);

            return BadRequest(new { Message = "Alguma propriedade não é valida" });
        }
    }
}
