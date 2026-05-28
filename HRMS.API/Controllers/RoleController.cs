using HRMS.Application.DTOs;
using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.ExtensionMapper;
using HRMS.Application.Interfaces;
using HRMS.Application.Services;
using HRMS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/role
        [HttpGet]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var result = await _roleService.GetAllRoleAsync();

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // POST: api/role
        [HttpPost]
        public async Task<IActionResult> AddRoleAsync([FromBody] RoleRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.AddRoleAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // PUT: api/role/{roleId}
        [HttpPut("{roleId:guid}")]
        public async Task<IActionResult> UpdateRoleAsync(
            Guid RoleId,
            [FromBody] RoleRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (RoleId != dto.RoleId)
                return BadRequest("RoleId mismatch.");

            var response = await _roleService.IsRoleExistAsync(RoleId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            var result = await _roleService.UpdateRoleAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // POST: api/role/assign-permissions
        [HttpPost("assign-permissions")]
        public IActionResult AssignPermissionsAsync(
            [FromBody] AssignRolePermissionRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =  _roleService.AssignPermissions(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}