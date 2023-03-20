using Eduversity.com.Server.Services.DepartmentHeadService;
using Eduversity.com.Shared.Dtos.DepartmentHeadDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentHeadsController : ControllerBase
    {
        private readonly IDepartmentHeadService _departmentHeadService;

        public DepartmentHeadsController(IDepartmentHeadService departmentHeadService)
        {
            _departmentHeadService = departmentHeadService;
        }

        [HttpGet("hods"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<DepartmentHeadResponse>>>> GetListOfDepartmentHeads()
        {
            var result = await _departmentHeadService.GetListOfDepartmentHeads();
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("hod"), Authorize(Roles = "HOD")]
        public async Task<ActionResult<ServiceResponse<DepartmentHeadResponse>>> GetDepartmentHead()
        {
            var result = await _departmentHeadService.GetDepartmentHead();
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("user/{userId}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<DepartmentHeadResponse>>> GetDepartmentHeadByUserId(long userId)
        {
            var result = await _departmentHeadService.GetDepartmentHeadByUserId(userId);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("department/{departmentId}"), Authorize(Roles = "Admin,Lecturer,Student")]
        public async Task<ActionResult<ServiceResponse<DepartmentHeadResponse>>> GetDepartmentHeadByDepartmentId(int departmentId)
        {
            var result = await _departmentHeadService.GetDepartmentHeadByDepartmentId(departmentId);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{headId}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<DepartmentHeadResponse>>> GetDepartmentHeadByHeadId(int headId)
        {
            var result = await _departmentHeadService.GetDepartmentHeadByHeadId(headId);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPost, Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<DepartmentHeadResponse>>> AddDepartmentHead(DepartmentHeadRequest request)
        {
            var result = await _departmentHeadService.AddDepartmentHead(request);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPut("{headId}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<DepartmentHeadResponse>>> UpdateDepartmentHead(int headId, DepartmentHeadRequest request)
        {
            var result = await _departmentHeadService.UpdateDepartmentHead(headId, request);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
