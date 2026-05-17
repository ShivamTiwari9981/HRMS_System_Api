using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [Authorize]
        [HttpPost("get-all-role")]
        public IActionResult GetAllRole()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _roleService.GetAllRole();

            if (!result.IsCompleted)
                return BadRequest(result);

            return Ok(result);
        }


        [Authorize]
        [HttpPost("add-role")]
        public IActionResult AddRole([FromBody] RoleRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =  _roleService.AddRole(dto);

            if (!result.IsCompleted)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("assign-permissions")]
        public IActionResult AssignPermissions(AssignRolePermissionRequestDto dto)
        {
            var result =  _roleService
                .AssignPermissions(dto);

            return Ok(result);
        }
    }
}
