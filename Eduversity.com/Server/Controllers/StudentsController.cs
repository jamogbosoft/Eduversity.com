using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<StudentResponse>>>> GetStudents()
        {
            var result = await _studentService.GetStudents();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("student")]
        //Authenticated User
        public async Task<ActionResult<ServiceResponse<StudentResponse>>> GetStudent()
        {
            var result = await _studentService.GetStudent();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("admin/{userId}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<StudentResponse>>> GetStudent(long userId)
        {
            var result = await _studentService.GetStudent(userId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }       

        [HttpGet("admin/option/{optionId}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<List<StudentResponse>>>> GetStudents(int optionId)
        {
            var result = await _studentService.GetStudents(optionId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("admin/program"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<List<StudentResponse>>>> GetStudents(int optionId, string currentSession, int currentLevel)
        {
            var result = await _studentService.GetStudents(optionId, currentSession, currentLevel);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPost, Authorize(Roles = "Admin,Student")]
        public async Task<ActionResult<ServiceResponse<StudentResponse>>> AddOrUpdateStudent(StudentResponse studentResponse)
        {
            var result =  await _studentService.AddOrUpdateStudent(studentResponse);
            return Ok(result);
        }

        [HttpDelete("{studentId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteStudent(long studentId)
        {
            var result = await _studentService.DeleteStudent(studentId);
            return Ok(result);
        }

        [HttpGet("admin/search/{searchText}/{page}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<StudentSearchResponse>>> SearchStudents(string searchText, int page)
        {
            var result = await _studentService.SearchStudents(searchText, page);
            return Ok(result);
        }

        [HttpGet("admin/option/{optionId}/search/{searchText}/{page}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<StudentSearchResponse>>> SearchStudents(string searchText, int page, int optionId)
        {
            var result = await _studentService.SearchStudents(searchText, page, optionId);
            return Ok(result);
        }

        [HttpGet("admin/searchsuggestions/{searchText}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetStudentSearchSuggestions(string searchText)
        {
            var result = await _studentService.GetStudentSearchSuggestions(searchText);
            return Ok(result);
        }

        [HttpGet("admin/option/{optionId}/searchsuggestions/{searchText}"), Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetStudentSearchSuggestions(string searchText, int optionId)
        {
            var result = await _studentService.GetStudentSearchSuggestions(searchText, optionId);
            return Ok(result);
        }
    }
}
