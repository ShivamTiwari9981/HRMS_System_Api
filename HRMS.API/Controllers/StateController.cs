using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IMasterDataService _masterService;

        public StateController(IMasterDataService masterService)
        {
            _masterService = masterService;
        }

        // GET: api/State
        [HttpGet]
        public async Task<IActionResult> GetAllStateAsync()
        {
            var result = await _masterService.GetAllStatesAsync();

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // POST: api/State
        [HttpPost]
        public async Task<IActionResult> AddStateAsync([FromBody] StateRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(dto.StateName))
                return BadRequest("State Name is required field.");

            var result = await _masterService.AddStateAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // PUT: api/State/{stateId}
        [HttpPut("{stateId:guid}")]
        public async Task<IActionResult> UpdateStateAsync(
            Guid StateId,
            [FromBody] StateRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (StateId != dto.StateId)
                return BadRequest("StateId mismatch.");

            var response = await _masterService.IsCountryExistById(StateId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            var result = await _masterService.UpdateStateAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // DELETE: api/State/{Stateid}
        [HttpDelete("{stateId:guid}")]
        public async Task<IActionResult> DeleteStateAsync(Guid StateId)
        {
            var response = await _masterService.DeactivateStateAsync(StateId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }

        // PUT: api/State/{StateId}/restore
        [HttpPut("{stateId:guid}/restore")]
        public async Task<IActionResult> RestoreStateAsync(Guid StateId)
        {
            var response = await _masterService.ActivateStateAsync(StateId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }
    }
}
