using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LecturersController : ControllerBase
    {
        private readonly ILecturerService _lecturerService;

        public LecturersController(ILecturerService lecturerService)
        {
            _lecturerService = lecturerService;
        }

        [HttpGet("admin"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<List<LecturerResponse>>>> GetLecturers()
        {
            var result = await _lecturerService.GetLecturers();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("admin/department/{departmentId}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<List<LecturerResponse>>>> GetLecturers(int departmentId)
        {
            var result = await _lecturerService.GetLecturers(departmentId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("lecturer")]
        //Authenticated User
        public async Task<ActionResult<ServiceResponse<LecturerResponse>>> GetLecturer()
        {
            var result = await _lecturerService.GetLecturer();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("admin/{userId}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<LecturerResponse>>> GetLecturer(long userId)
        {
            var result = await _lecturerService.GetLecturer(userId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPost, Authorize(Roles = "Admin,Lecturer")]
        public async Task<ActionResult<ServiceResponse<LecturerResponse>>> AddOrUpdateLecturer(LecturerResponse lecturerResponse)
        {
            var result = await _lecturerService.AddOrUpdateLecturer(lecturerResponse);
            return Ok(result);
        }

        [HttpDelete("{lecturerId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteLecturer(int lecturerId)
        {
            var result = await _lecturerService.DeleteLecturer(lecturerId);
            return Ok(result);
        }

        [HttpGet("admin/search/{searchText}/{page}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<LecturerSearchResponse>>> SearchLecturers(string searchText, int page)
        {
            var result = await _lecturerService.SearchLecturers(searchText, page);
            return Ok(result);
        }

        [HttpGet("admin/department/{departmentId}/search/{searchText}/{page}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<LecturerSearchResponse>>> SearchLecturers(string searchText, int page, int departmentId)
        {
            var result = await _lecturerService.SearchLecturers(searchText, page, departmentId);
            return Ok(result);
        }

        [HttpGet("admin/searchsuggestions/{searchText}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult< ServiceResponse<List<string>>>> GetLecturerSearchSuggestions(string searchText)
        {
            var result = await _lecturerService.GetLecturerSearchSuggestions(searchText);
            return Ok(result);
        }

        [HttpGet("admin/department/{departmentId}/searchsuggestions/{searchText}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetLecturerSearchSuggestions(string searchText, int departmentId)
        {
            var result = await _lecturerService.GetLecturerSearchSuggestions(searchText, departmentId);
            return Ok(result);
        }
    }
}
