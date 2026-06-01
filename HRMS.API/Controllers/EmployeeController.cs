using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.Interfaces;
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
