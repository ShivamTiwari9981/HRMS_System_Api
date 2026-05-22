using HRMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterDataService _masterService;
        public MasterController(IMasterDataService masterService)
        {
            _masterService = masterService;
        }


        [HttpGet("company-dropdown")]
        public async Task<IActionResult> PlanAndCompanyType()
        {
            var result = await _masterService.GetPlanAndCompanyTypeAsync();
            return Ok(result);
        }
    }
}
