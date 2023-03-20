using Eduversity.com.Server.ResponseExtensions;

namespace Eduversity.com.Server.Services.CourseAllocationService
{
    public class CourseAllocationService : ICourseAllocationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CourseAllocationService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<CourseAllocationResponse>> AddCourseAllocation(CourseAllocationRequest courseAllocationRequest)
        {
            var courseAllocation = _mapper.Map<CourseAllocation>(courseAllocationRequest);

            _context.CourseAllocations.Add(courseAllocation);
            await _context.SaveChangesAsync();

            var courseAllocationResponse = _mapper.Map<CourseAllocationResponse>(courseAllocation);
            return new ServiceResponse<CourseAllocationResponse>()
            {
                Data = courseAllocationResponse
            };
        }

        public async Task<ServiceResponse<List<CourseAllocationResponse>>> GetCoursesAllocatedToLecturer(int lecturerId)
        {
            // Using extension method
            var courseAllocation = await _context.CourseAllocations
                    .ToCourseAllocationResponse()
                    .Where(c => c.LecturerId == lecturerId)
                    .OrderBy(c => c.Session)
                    .ThenBy(c => c.CourseCode)
                    .ToListAsync();

            var response = new ServiceResponse<List<CourseAllocationResponse>>
            {
                Data = courseAllocation
            };
            return response;
        }

        public async Task<ServiceResponse<List<CourseAllocationResponse>>> GetCoursesAllocatedToLecturer(int lecturerId, string session)
        {
            // Using extension method
            var courseAllocation = await _context.CourseAllocations
                    .ToCourseAllocationResponse()
                    .Where(c => c.LecturerId == lecturerId && c.Session == session)
                    .OrderBy(c => c.CourseCode)
                    .ToListAsync();

            var response = new ServiceResponse<List<CourseAllocationResponse>>
            {
                Data = courseAllocation
            };
            return response;
        }

        public async Task<ServiceResponse<List<CourseAllocationResponse>>> GetLecturersAllocatedToCourse(int courseId, string session)
        {
            // Using extension method
            var courseAllocation = await _context.CourseAllocations
                    .ToCourseAllocationResponse()
                    .Where(c => c.CourseId == courseId && c.Session == session)
                    .OrderBy(c => c.LecturerName)
                    .ToListAsync();

            var response = new ServiceResponse<List<CourseAllocationResponse>>
            {
                Data = courseAllocation
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> RemoveCourseAllocation(CourseAllocationRequest courseAllocationRequest)
        { 
            var request = courseAllocationRequest;
            var dbcourseAllocation = await _context.CourseAllocations
                .FirstOrDefaultAsync(c => c.LecturerId == request.LecturerId && c.CourseId == request.CourseId && c.Session == request.Session);

            if (dbcourseAllocation is null)
            {
                return new ServiceResponse<bool>()
                {
                    Data = false,
                    Success = false,
                    Message = "This course does not exist."
                };
            }

            _context.CourseAllocations.Remove(dbcourseAllocation);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>()
            {
                Data = true
            };
        }
    }
}
