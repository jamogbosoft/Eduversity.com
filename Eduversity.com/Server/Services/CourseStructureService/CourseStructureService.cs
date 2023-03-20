using Eduversity.com.Server.ResponseExtensions;

namespace Eduversity.com.Server.Services.CourseStructureService
{
    public class CourseStructureService : ICourseStructureService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CourseStructureService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<CourseStructureResponse>> AddCourse(CourseStructureRequest courseStructureRequest)
        {
            var course = _mapper.Map<CourseStructure>(courseStructureRequest);

            _context.CourseStructures.Add(course);
            await _context.SaveChangesAsync();

            var courseStructureResponse = _mapper.Map<CourseStructureResponse>(course);
            return new ServiceResponse<CourseStructureResponse>()
            {
                Data = courseStructureResponse
            };
        }

        public async Task<ServiceResponse<List<CourseStructureResponse>>> GetListOfCourses(int optionId)
        {
            // Using extension method
            var courses = await _context.CourseStructures                   
                    .Where(c => c.DepartmentOptionId == optionId)
                    .OrderBy(c => c.Level)
                    .ThenBy(c => c.Semester)
                    .ToCourseStructureResponse()
                    .ToListAsync();

            var response = new ServiceResponse<List<CourseStructureResponse>>
            {
                Data = courses
            };
            return response;
        }

        public async Task<ServiceResponse<List<CourseStructureResponse>>> GetListOfCourses(int optionId, int level, string semester)
        {
            // Using extension method
            List<CourseStructureResponse>? courses = null;
            if (semester == "Summer")
            {
                courses = await _context.CourseStructures
                    .Where(c => c.DepartmentOptionId == optionId && c.Level == level)
                    .ToCourseStructureResponse()
                    .ToListAsync();
            }
            else
            {
                courses = await _context.CourseStructures
                   .Where(c => c.DepartmentOptionId == optionId && c.Level == level && c.Semester == semester)
                   .ToCourseStructureResponse()
                   .ToListAsync();
            }
            var response = new ServiceResponse<List<CourseStructureResponse>>
            {
                Data = courses
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> RemoveCourse(long structureId)
        {
            var dbCourse = await _context.CourseStructures.FindAsync(structureId);
            if (dbCourse is null)
            {
                return new ServiceResponse<bool>()
                {
                    Data = false,
                    Success = false,
                    Message = "This course does not exist."
                };
            }

            _context.CourseStructures.Remove(dbCourse);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>()
            {
                Data = true
            };
        }
    }
}
