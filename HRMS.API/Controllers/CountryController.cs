using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMasterDataService _masterService;

        public CountryController(IMasterDataService masterService)
        {
            _masterService = masterService;
        }

        // GET: api/country
        [HttpGet]
        public async Task<IActionResult> GetAllCountryAsync()
        {
            var result = await _masterService.GetAllCountriesAsync();

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // POST: api/country
        [HttpPost]
        public async Task<IActionResult> AddCountryAsync([FromBody] CountryRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = dto.GetEntity();

            var result = await _masterService.SaveAsync(entity);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // PUT: api/country/{CountryId}
        [HttpPut("{CountryId:guid}")]
        public async Task<IActionResult> UpdateCountryAsync(
            Guid CountryId,
            [FromBody] CountryRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (CountryId != dto.CountryId)
                return BadRequest("CountryId mismatch.");

            var response = await _masterService.IsCountryExistById(CountryId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            var entity = dto.GetEntity();

            var result = await _masterService.UpdateAsync(entity);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // DELETE: api/country/{Countryid}
        [HttpDelete("{CountryId:guid}")]
        public async Task<IActionResult> DeleteCountryAsync(Guid CountryId)
        {
            var response = await _masterService.DeactivateAsync(CountryId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }

        // PUT: api/Country/{CountryId}/restore
        [HttpPut("{CountryId:guid}/restore")]
        public async Task<IActionResult> RestoreCountryAsync(Guid CountryId)
        {
            var response = await _masterService.RepopenAsync(CountryId);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }


    }
}
