using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.Interfaces;
using HRMS.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }


        [Authorize]
        [HttpGet("get-all-permission")]
        public IActionResult GetAllPermission()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _permissionService.GetAllPermission();

            if (!result.IsCompleted)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("get-permissionById")]
        public IActionResult GetPermissionById(Guid PermissionId)
        {
            if(string.IsNullOrEmpty(PermissionId.ToString()))
                return BadRequest("PermissionId is required !");

            var result = _permissionService.GetPermissionById(PermissionId);

            if (!result.IsCompleted)
                return BadRequest(result);

            return Ok(result);
        }


        [Authorize]
        [HttpPost("add-permission")]
        public IActionResult AddPermission([FromBody] PermissionRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _permissionService.AddPermission(dto);

            if (!result.IsCompleted)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
