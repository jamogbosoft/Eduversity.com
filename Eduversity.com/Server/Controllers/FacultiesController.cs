using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly IFacultyService _facultyService;

        public FacultiesController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }
        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<Faculty>>>> GetAdminFaculties()
        {
            var result = await _facultyService.GetAdminFaculties();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<FacultyReadDto>>>> GetFaculties()
        {
            var result = await _facultyService.GetFaculties();
            return Ok(result);
        }

        [HttpGet("admin/{FacultyId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Faculty>>> GetAdminFaculty(int facultyId)
        {
            var result = await _facultyService.GetAdminFaculty(facultyId);
            return Ok(result);
        }

        [HttpGet("{facultyId}")]
        public async Task<ActionResult<ServiceResponse<FacultyReadDto>>> GetFaculty(int facultyId)
        {
            var result = await _facultyService.GetFaculty(facultyId);
            return Ok(result);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Faculty>>> CreateFaculty(Faculty faculty)
        {
            var result = await _facultyService.CreateFaculty(faculty);
            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Faculty>>> UpdateFaculty(Faculty faculty)
        {
            var result = await _facultyService.UpdateFaculty(faculty);
            return Ok(result);
        }

        [HttpDelete("{facultyId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteFaculty(int facultyId)
        {
            var result = await _facultyService.DeleteFaculty(facultyId);
            return Ok(result);
        }
    }
}
