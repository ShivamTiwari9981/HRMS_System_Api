using HRMS.Application.DTOs;
using HRMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/department
        [HttpGet]
        public async Task<IActionResult> GetAllDepartmentsAsync()
        {
            var result = await _departmentService.GetAllDepartmentsAsync();

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // GET: api/department/{departmentId}
        [HttpGet("{departmentId:guid}")]
        public async Task<IActionResult> GetDepartmentByIdAsync(Guid departmentId)
        {
            var result = await _departmentService.GetDepartmentByIdAsync(departmentId);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        // POST: api/department
        [HttpPost]
        public async Task<IActionResult> SaveDepartmentAsync([FromBody] DepartmentDto department)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = department.GetEntity();

            var result = await _departmentService.SaveAsync(entity);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // PUT: api/department/{departmentId}
        [HttpPut("{departmentId:guid}")]
        public async Task<IActionResult> UpdateDepartmentAsync(
            Guid departmentId,
            [FromBody] DepartmentDto department)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (departmentId != department.DepartmentId)
                return BadRequest("DepartmentId mismatch.");

            var response = await _departmentService.IsDepartmentExist(departmentId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            var entity = department.GetEntity();

            var result = await _departmentService.UpdateAsync(entity);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // DELETE: api/department/{departmentId}
        [HttpDelete("{departmentId:guid}")]
        public async Task<IActionResult> DeleteDepartmentAsync(Guid departmentId)
        {
            var response = await _departmentService.DeactivateAsync(departmentId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }

        // PUT: api/department/{departmentId}/restore
        [HttpPut("{departmentId:guid}/restore")]
        public async Task<IActionResult> RestoreDepartmentAsync(Guid departmentId)
        {
            var response = await _departmentService.RepopenAsync(departmentId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }
    }
}