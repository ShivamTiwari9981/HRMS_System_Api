using HRMS.Application.DTOs;
using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Signup")]
        public IActionResult SignUp([FromBody] SignupRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _authService.UserSignUp(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

                var result =  _authService.Login(dto);

                if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromQuery] string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                return BadRequest("User email is required");
            }

            var result = await _authService.SendOtpAsync(userEmail);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("verify-otp")]
        public async Task<IActionResult> VerifyOTP([FromQuery] string UserEmail, string OTP)
        {
            if (string.IsNullOrWhiteSpace(UserEmail))
            {
                return BadRequest("User email is required");
            }
            if (string.IsNullOrWhiteSpace(OTP))
            {
                return BadRequest("OTP is required");
            }

            var result = await _authService.VerifyEmailOTP(UserEmail, OTP);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ForgetPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.ResetPassword(dto.UserEmail, dto.Password);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
