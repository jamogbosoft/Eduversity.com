using Eduversity.com.Shared.Dtos.DepartmentHeadDto;

namespace Eduversity.com.Server.ResponseExtensions
{
    public static class DepartmentHeadResponseExtensions
    {
        public static IQueryable<DepartmentHeadResponse> ToDepartmentHeadResponse(this IQueryable<DepartmentHead> source)
        {
            return source.Select(h => new DepartmentHeadResponse
            {
                Id = h.Id,
                UserId = h.UserId,
                LecturerId = h.LecturerId,
                DepartmentId = h.DepartmentId,
                Name = h.Lecturer!.Name,
                DepartmentName = h.Department!.Name
            });
        }
    }
}
