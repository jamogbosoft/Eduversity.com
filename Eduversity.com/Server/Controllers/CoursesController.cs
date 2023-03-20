using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<CourseResponse>>>> GetCourses()
        {
            var result = await _courseService.GetCourses();
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{courseId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<CourseResponse>>> GetCourseById(int courseId)
        {
            var result = await _courseService.GetCourseById(courseId);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("deleted"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<CourseResponse>>>> GetDeletedCourses()
        {
            var result = await _courseService.GetDeletedCourses();
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<CourseResponse>>> CreateCourse(CourseRequest courseRequest)
        {
            var result = await _courseService.CreateCourse(courseRequest);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPut("{courseId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<CourseResponse>>> UpdateCourse(int courseId, CourseRequest courseRequest)
        {
            var result = await _courseService.UpdateCourse(courseId, courseRequest);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpDelete("{courseId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCourse(int courseId)
        {
            var result = await _courseService.DeleteCourse(courseId);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("search/{searchText}/{page}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<CourseSearchResponse>>> SearchCourses(string searchText, int page = 1)
        {
            var result = await _courseService.SearchCourses(searchText, page);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("searchsuggestions/{searchText}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetCoursesSearchSuggestions(string searchText)
        {
            var result = await _courseService.GetCourseSearchSuggestions(searchText);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
