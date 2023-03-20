using Eduversity.com.Shared.Dtos.CourseStructureDto;
using Eduversity.com.Shared.Dtos.DepartmentHeadDto;

namespace Eduversity.com.Server.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Source -> Target
            CreateMap<Country, CountryReadDto>(); //Read
            //CreateMap<CountryCreateDto, Country>(); //Write
            //CreateMap<CountryUpdateDto, Country>(); //Write

            CreateMap<State, StateReadDto>(); //Read
            CreateMap<LGA, LGAReadDto>(); //Read

            CreateMap<Faculty, FacultyReadDto>();
            CreateMap<Department, DepartmentReadDto>();
            CreateMap<DepartmentOption, DepartmentOptionReadDto>();

            CreateMap<UserAddressResponse, UserAddress>(); //Write

            CreateMap<Student, StudentResponse>(); //Read
            CreateMap<StudentResponse, Student>(); //Write            

            CreateMap<StudentAcademicDetail, StudentAcademicDetailResponse>(); //Read
            CreateMap<StudentAcademicDetailResponse, StudentAcademicDetail>(); //Write

            CreateMap<Lecturer, LecturerResponse>(); //Read
            CreateMap<LecturerResponse, Lecturer>(); //Write            

            CreateMap<LecturerAcademicDetail, LecturerAcademicDetailResponse>(); //Read
            CreateMap<LecturerAcademicDetailResponse, LecturerAcademicDetail>(); //Write

            CreateMap<Qualification, QualificationResponse>(); //Read
            CreateMap<QualificationResponse, Qualification>(); //Write     

            CreateMap<Course, CourseResponse>(); //Read
            CreateMap<CourseRequest, Course>(); //Write    

            CreateMap<CourseStructure, CourseStructureResponse>(); //Read
            CreateMap<CourseStructureRequest, CourseStructure>(); //Write   

            CreateMap<CourseAllocation, CourseAllocationResponse>(); //Read
            CreateMap<CourseAllocationRequest, CourseAllocation>(); //Write   

            CreateMap<DepartmentHead, DepartmentHeadResponse>(); //Read
            CreateMap<DepartmentHeadRequest, DepartmentHead>(); //Write   
        }
    }
}
