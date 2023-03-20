using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LGAsController : ControllerBase
    {
        private readonly ILGAService _lgaService;

        public LGAsController(ILGAService lgaService) 
        {
            _lgaService = lgaService;
        }

        [HttpGet("admin-sId/{stateId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<LGAsResponse>>> GetAdminLGAs(int stateId)
        {
            var result = await _lgaService.GetAdminLGAs(stateId);
            return Ok(result);
        }

        [HttpGet("user-sId/{stateId}")]
        public async Task<ActionResult<ServiceResponse<List<LGAReadDto>>>> GetLGAs(int stateId)
        {
            var result = await _lgaService.GetLGAs(stateId);
            return Ok(result);
        }

        [HttpGet("admin-lId/{lgaId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<LGAResponse>>> GetAdminLGA(int lgaId)
        {
            var result = await _lgaService.GetAdminLGA(lgaId);
            return Ok(result);
        }

        [HttpGet("user-lId/{lgaId}")]
        public async Task<ActionResult<ServiceResponse<LGAResponse>>> GetLGA(int lgaId)
        {
            var result = await _lgaService.GetLGA(lgaId);
            return Ok(result);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<LGA>>> CreateLGA(LGA lga)
        {
            var result = await _lgaService.CreateLGA(lga);
            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<LGA>>> UpdateLGA(LGA lga)
        {
            var result = await _lgaService.UpdateLGA(lga); 
            return Ok(result);
        }

        [HttpDelete("{lgaId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteLGA(int lgaId)
        {
            var result = await _lgaService.DeleteLGA(lgaId);
            return Ok(result);
        }

    }
}
