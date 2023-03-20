using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentOptionsController : ControllerBase
    {
        private readonly IDepartmentOptionService _optionService;

        public DepartmentOptionsController(IDepartmentOptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpGet("admin-dId/{departmentId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<DepartmentOptionsResponse>>> GetAdminDepartmentOptions(int departmentId)
        {
            var result = await _optionService.GetAdminDepartmentOptions(departmentId);
            return Ok(result);
        }

        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<DepartmentOptionReadDto>>>> GetDepartmentOptions()
        {
            var result = await _optionService.GetDepartmentOptions();
            return Ok(result);
        }

        [HttpGet("user-dId/{departmentId}")]
        public async Task<ActionResult<ServiceResponse<List<DepartmentOptionReadDto>>>> GetDepartmentOptions(int departmentId)
        {
            var result = await _optionService.GetDepartmentOptions(departmentId);
            return Ok(result);
        }

        [HttpGet("admin-oId/{optionId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<DepartmentOptionResponse>>> GetAdminDepartmentOption(int optionId)
        {
            var result = await _optionService.GetAdminDepartmentOption(optionId);
            return Ok(result);
        }

        [HttpGet("user-oId/{optionId}")]
        public async Task<ActionResult<ServiceResponse<DepartmentOptionResponse>>> GetDepartmentOption(int optionId)
        {
            var result = await _optionService.GetDepartmentOption(optionId);
            return Ok(result);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<DepartmentOption>>> CreateDepartmentOption(DepartmentOption option)
        {
            var result = await _optionService.CreateDepartmentOption(option);
            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<DepartmentOption>>> UpdateDepartmentOption(DepartmentOption option)
        {
            var result = await _optionService.UpdateDepartmentOption(option);
            return Ok(result);
        }

        [HttpDelete("{optionId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteDepartmentOption(int optionId)
        {
            var result = await _optionService.DeleteDepartmentOption(optionId);
            return Ok(result);
        }

    }
}
