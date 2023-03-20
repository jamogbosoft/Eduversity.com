using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseAllocationsController : ControllerBase
    {
        private readonly ICourseAllocationService _courseAllocationService;

        public CourseAllocationsController(ICourseAllocationService courseAllocationService)
        {
            _courseAllocationService = courseAllocationService;
        }

        [HttpGet("lecturer/{lecturerId}")]
        public async Task<ActionResult<ServiceResponse<List<CourseAllocationResponse>>>> GetCoursesAllocatedToLecturer(int lecturerId)
        {
            var result = await _courseAllocationService.GetCoursesAllocatedToLecturer(lecturerId);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("lecturer-session")]
        public async Task<ActionResult<ServiceResponse<List<CourseAllocationResponse>>>> GetCoursesAllocatedToLecturer(int lecturerId, string session)
        {
            var result = await _courseAllocationService.GetCoursesAllocatedToLecturer(lecturerId, session);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("course-session")]
        public async Task<ActionResult<ServiceResponse<List<CourseAllocationResponse>>>> GetLecturersAllocatedToCourse(int courseId, string session)
        {
            var result = await _courseAllocationService.GetLecturersAllocatedToCourse(courseId, session);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPost, Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<CourseAllocationResponse>>> AddCourseAllocation(CourseAllocationRequest courseAllocationRequest)
        {
            var result = await _courseAllocationService.AddCourseAllocation(courseAllocationRequest);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpDelete, Authorize(Roles = "Admin,HOD")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveCourseAllocation(CourseAllocationRequest courseAllocationRequest)
        {
            var result = await _courseAllocationService.RemoveCourseAllocation(courseAllocationRequest);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
