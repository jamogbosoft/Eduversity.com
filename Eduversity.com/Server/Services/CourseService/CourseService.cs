namespace Eduversity.com.Server.Services.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CourseService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CourseResponse>> CreateCourse(CourseRequest courseRequest)
        {
            var course = _mapper.Map<Course>(courseRequest);

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var courseResponse = _mapper.Map<CourseResponse>(course);
            return new ServiceResponse<CourseResponse>()
            {
                Data = courseResponse
            };
        }

        public async Task<ServiceResponse<bool>> DeleteCourse(int courseId)
        {
            var dbCourse = await _context.Courses.FindAsync(courseId);
            if (dbCourse is null)
            {
                return new ServiceResponse<bool>()
                {
                    Data = false,
                    Success = false,
                    Message = "This course does not exist."
                };
            }

            //_context.Courses.Remove(dbCourse); \\
            dbCourse.IsDeleted = true;
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>()
            {
                Data = true
            };
        }

        public async Task<ServiceResponse<CourseResponse>> GetCourseById(int courseId)
        {
            //var dbCourse = await _context.Courses.FindAsync(courseId); \\
            var dbCourse =
                await _context.Courses
                    .FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == courseId);
            var courseResponse = _mapper.Map<CourseResponse>(dbCourse);
            return new ServiceResponse<CourseResponse>()
            {
                Data = courseResponse
            };
        }

        public async Task<ServiceResponse<List<CourseResponse>>> GetCourses()
        {
            var dbCourses =
                await _context.Courses
                    .Where(c => !c.IsDeleted)
                    .OrderBy(c => c.Code)
                    .ToListAsync();
            var courses = _mapper.Map<List<CourseResponse>>(dbCourses);
            return new ServiceResponse<List<CourseResponse>>()
            {
                Data = courses
            };
        }

        public async Task<ServiceResponse<List<CourseResponse>>> GetDeletedCourses()
        {
            var dbCourses =
                await _context.Courses
                    .Where(c => c.IsDeleted)
                    .OrderBy(c => c.Code)
                    .ToListAsync();
            var courses = _mapper.Map<List<CourseResponse>>(dbCourses);
            return new ServiceResponse<List<CourseResponse>>()
            {
                Data = courses
            };
        }

        public async Task<ServiceResponse<CourseResponse>> UpdateCourse(int courseId, CourseRequest courseRequest)
        {
            var dbCourse = await _context.Courses.FindAsync(courseId);
            if (dbCourse is null)
            {
                return new ServiceResponse<CourseResponse>()
                {
                    Data = null,
                    Success = false,
                    Message = "This course does not exist."
                };
            }

            dbCourse.Code = courseRequest.Code;
            dbCourse.Title = courseRequest.Title;
            dbCourse.Unit = courseRequest.Unit;
            dbCourse.Status = courseRequest.Status;
            dbCourse.IsActive = courseRequest.IsActive;
            dbCourse.IsDeleted = courseRequest.IsDeleted;

            await _context.SaveChangesAsync();

            var courseResponse = _mapper.Map<CourseResponse>(dbCourse);
            return new ServiceResponse<CourseResponse>()
            {
                Data = courseResponse
            };
        }

        public async Task<ServiceResponse<List<string>>> GetCourseSearchSuggestions(string searchText)
        {
            var courses = await FindCoursesBySearchText(searchText);

            List<string> result = new List<string>();

            foreach (var course in courses)
            {
                if (course.Code.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                    && !result.Contains(course.Code))
                {
                    result.Add(course.Code);
                }
                if (course.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                    && !result.Contains(course.Title))
                {
                    result.Add(course.Title);

                }
            }
            return new ServiceResponse<List<string>> { Data = result };
        }

        public async Task<ServiceResponse<CourseSearchResponse>> SearchCourses(string searchText, int page, int pageSize)
        {
            var result = await FindCoursesBySearchText(searchText);

            var rocordCount = (float)pageSize;
            var pageCount = Math.Ceiling(result.Count / rocordCount);
            var courses = result
                    .Skip((page - 1) * (int)rocordCount)
                    .Take((int)rocordCount)
                    .ToList();

            var response = new ServiceResponse<CourseSearchResponse>
            {
                Data = new CourseSearchResponse
                {
                    Courses = courses,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        private async Task<List<CourseResponse>> FindCoursesBySearchText(string searchText)
        {
            var allCourses = await GetCourses();
            var courses = allCourses.Data != null ? allCourses.Data
                .Where(c => c.Code.ToLower().Contains(searchText.ToLower()) ||
                       c.Title.ToLower().Contains(searchText.ToLower()))
                .ToList() : new List<CourseResponse>();
            return courses;
        }
    }
}
