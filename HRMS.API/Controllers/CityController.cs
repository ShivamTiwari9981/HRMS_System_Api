using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMasterDataService _masterService;

        public CityController(IMasterDataService masterService)
        {
            _masterService = masterService;
        }

        // GET: api/City
        [HttpGet]
        public async Task<IActionResult> GetAllCityAsync()
        {
            var result = await _masterService.GetAllCityAsync();

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // POST: api/City
        [HttpPost]
        public async Task<IActionResult> AddCityAsync([FromBody] CityRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _masterService.AddCityAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // PUT: api/City/{cityId}
        [HttpPut("{cityId:guid}")]
        public async Task<IActionResult> UpdateCityAsync(
            Guid CityId,
            [FromBody] CityRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (CityId != dto.CityId)
                return BadRequest("CityId mismatch.");

            var response = await _masterService.IsCityExistById(CityId);

            if (!response.IsSuccess)
                return NotFound(response.Message);


            var result = await _masterService.UpdateCityAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // DELETE: api/City/{cityId}
        [HttpDelete("{cityId:guid}")]
        public async Task<IActionResult> DeleteCityAsync(Guid CityId)
        {
            var response = await _masterService.DeactivateCityAsync(CityId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }

        // PUT: api/City/{CityId}/restore
        [HttpPut("{cityId:guid}/activate")]
        public async Task<IActionResult> ActivateCityAsync(Guid CityId)
        {
            var response = await _masterService.ActivateCityAsync(CityId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }
    }
}
