using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseStructuresController : ControllerBase
    {
        private readonly ICourseStructureService _courseStructureService;

        public CourseStructuresController(ICourseStructureService courseStructureService)
        {
            _courseStructureService = courseStructureService;
        }

        [HttpGet("option/{optionId}")]
        public async Task<ActionResult<ServiceResponse<List<CourseStructureResponse>>>> GetListOfCourses(int optionId)
        {
            var result = await _courseStructureService.GetListOfCourses(optionId);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("option/{optionId}/{level}/{semester}")]
        public async Task<ActionResult<ServiceResponse<List<CourseStructureResponse>>>> GetListOfCourses(int optionId, int level, string semester)
        {
            var result = await _courseStructureService.GetListOfCourses(optionId, level, semester);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<CourseStructureResponse>>> AddCourse(CourseStructureRequest courseStructureRequest)
        {
            var result = await _courseStructureService.AddCourse(courseStructureRequest);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpDelete("{structureId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveCourse(long structureId)
        {
            var result = await _courseStructureService.RemoveCourse(structureId);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
