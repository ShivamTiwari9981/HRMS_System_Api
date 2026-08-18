using HRMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _leaveService;

        public LeaveController(ILeaveService masterService)
        {
            _leaveService = masterService;
        }


        // GET: api/leave/{departmentId}
        [HttpGet("{employeeId:guid}")]
        public async Task<IActionResult> GetDepartmentByIdAsync(Guid employeeId)
        {
            var result = await _leaveService.GetAllLeaveBalanceAsync(employeeId);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
    }
}
