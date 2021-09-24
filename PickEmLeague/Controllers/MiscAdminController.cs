using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.DomainServices.Interfaces;

namespace PickEmLeague.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MiscAdminController : ControllerBase
    {
        private readonly IMiscAdminService _miscAdminService;

        public MiscAdminController(IMiscAdminService miscAdminService)
        {
            _miscAdminService = miscAdminService;
        }

        [HttpGet("getMiscAdmin")]
        public async Task<MiscAdmin> GetMiscAdmin()
        {
            return await _miscAdminService.GetMiscAdminAsync();
        }

        [HttpPut("updateMiscAdmin")]
        public async Task SetMiscAdmin(MiscAdmin miscAdmin)
        {
            await _miscAdminService.UpdateMiscAdminAsync(miscAdmin);
        }
    }
}
