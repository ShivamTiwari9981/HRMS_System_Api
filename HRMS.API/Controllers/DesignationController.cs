using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;

        public DesignationController(IDesignationService DesignationService)
        {
            _designationService = DesignationService;
        }

        // GET: api/Designation
        [HttpGet]
        public async Task<IActionResult> GetAllDesignationsAsync()
        {
            var result = await _designationService.GetAllDesignationsAsync();

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // GET: api/Designation/{DesignationId}
        [HttpGet("{DesignationId:guid}")]
        public async Task<IActionResult> GetDesignationByIdAsync(Guid DesignationId)
        {
            var result = await _designationService.GetDesignationByIdAsync(DesignationId);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        // POST: api/Designation
        [HttpPost]
        public async Task<IActionResult> AddDesignationAsync([FromBody] DesignationRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _designationService.AddDesignationAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // PUT: api/Designation/{DesignationId}
        [HttpPut("{DesignationId:guid}")]
        public async Task<IActionResult> UpdateDesignationAsync(
            Guid DesignationId,
            [FromBody] DesignationRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (DesignationId != dto.DesignationId)
                return BadRequest("DesignationId mismatch.");

            var response = await _designationService.IsDesignationExist(DesignationId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            var result = await _designationService.UpdateDesignationAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // DELETE: api/Designation/{DesignationId}
        [HttpDelete("{DesignationId:guid}")]
        public async Task<IActionResult> DeleteDesignationAsync(Guid DesignationId)
        {
            var response = await _designationService.DeactivateDesignationAsync(DesignationId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }

        // PUT: api/Designation/{DesignationId}/restore
        [HttpPut("{DesignationId:guid}/restore")]
        public async Task<IActionResult> RestoreDesignationAsync(Guid DesignationId)
        {
            var response = await _designationService.ActivateDesignationAsync(DesignationId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }
    }
}
