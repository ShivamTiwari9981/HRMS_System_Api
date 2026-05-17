using HRMS.Application.DTOs;
using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.Interfaces;
using HRMS.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IAuthService _authService;
        public ClientController(IClientService clientService, IAuthService authService)
        {
            _clientService = clientService;
            _authService = authService;
        }

        [Authorize]
        [HttpPost("register-client")]
        public IActionResult RegisterClient([FromBody] ClientRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _clientService.RegisterClient(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            if(result.IsSuccess)
            {
                var data =_authService.GetUserRolePermissionsAsync(result.Data.ClientId,result.Data.UserId);
                return Ok(data);
            }
            return BadRequest(result);
        }
    }
}
