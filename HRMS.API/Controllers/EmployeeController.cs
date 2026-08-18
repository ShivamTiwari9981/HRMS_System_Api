using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.Interfaces;
using HRMS.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("search")]
        public async Task<IActionResult>GetEmployees([FromBody] EmployeeListRequestDto dto)
        {
            var result = await _employeeService.GetAllEmployees(dto);
            return Ok(result);
        }

        [HttpGet("load-dropdown")]
        public IActionResult GetDropdownList()
        {
            var result =  _employeeService.GetDropdownList();
            return Ok(result);
        }

        // GET: api/department/{departmentId}
        [HttpGet("{departmentId:guid}")]
        public async Task<IActionResult> GetDepartmentByIdAsync(Guid departmentId)
        {
            var result = await _employeeService.g(departmentId);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        // PUT: api/employee/{employeeId}
        [HttpPut("{employeeId:guid}")]
        public async Task<IActionResult> UpdateEmpoyeeAsync(
            Guid departmentId,
            [FromBody] DepartmentRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (departmentId != dto.DepartmentId)
                return BadRequest("DepartmentId mismatch.");

            var response = await _employeeService.IsDepartmentExist(departmentId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            var result = await _employeeService.UpdateDepartmentAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // DELETE: api/department/{departmentId}
        [HttpDelete("{departmentId:guid}")]
        public async Task<IActionResult> DeleteDepartmentAsync(Guid departmentId)
        {
            var response = await _employeeService.DeactivateDepartmentAsync(departmentId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }


        // PUT: api/department/{departmentId}/activate
        [HttpPatch("activate/{departmentId:guid}")]
        public async Task<IActionResult> ActivateDepartmentAsync(Guid departmentId)
        {
            var response = await _employeeService.ActivateDepartmentAsync(departmentId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }
    
        [HttpPost("add-employee")]
        public IActionResult EmployeeClient([FromBody] EmployeeRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =  _employeeService.AddEmployee(dto);

            if (!result.IsSuccess)
                return BadRequest(result);


            return Ok(result);
        }


    }
}
