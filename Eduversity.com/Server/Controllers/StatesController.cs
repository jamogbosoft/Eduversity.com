using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StatesController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet("admin-cId/{countryId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<StatesResponse>>> GetAdminStates(int countryId)
        {
            var result = await _stateService.GetAdminStates(countryId);
            return Ok(result);
        }

        [HttpGet("user-cId/{countryId}")]
        public async Task<ActionResult<ServiceResponse<List<StateReadDto>>>> GetStates(int countryId)
        {
            var result = await _stateService.GetStates(countryId);
            return Ok(result);
        }

        [HttpGet("admin-sId/{stateId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<StateResponse>>> GetAdminState(int stateId)
        {
            var result = await _stateService.GetAdminState(stateId);
            return Ok(result);
        }

        [HttpGet("user-sId/{stateId}")]
        public async Task<ActionResult<ServiceResponse<StateReadDto>>> GetState(int stateId)
        {
            var result = await _stateService.GetState(stateId);
            return Ok(result);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<State>>> CreateState(State state)
        {
            var result = await _stateService.CreateState(state);
            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<State>>> UpdateState(State state)
        {
            var result = await _stateService.UpdateState(state);
            return Ok(result);
        }

        [HttpDelete("{stateId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteState(int stateId)
        {
            var result = await _stateService.DeleteState(stateId);
            return Ok(result);
        }

    }
}
