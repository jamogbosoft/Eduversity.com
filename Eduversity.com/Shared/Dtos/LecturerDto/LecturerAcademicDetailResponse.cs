using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.LecturerDto
{
    public class LecturerAcademicDetailResponse
    {
        public int LecturerId { get; set; }
        public int FacultyId { get; set; }
        public int DepartmentId { get; set; }
        [MaxLength(70, ErrorMessage = "Designation should not exceed 70 characters.")]
        public string Designation { get; set; } = string.Empty;
        public string Specialisation { get; set; } = string.Empty;
        public string Certifications { get; set; } = string.Empty;
        public string Activities { get; set; } = string.Empty;
        public string ProfessionalAffiliation { get; set; } = string.Empty;
        public string Award { get; set; } = string.Empty;       
        public string FacultyName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string Memo { get; set; } = string.Empty;
        public List<QualificationResponse> Qualifications { get; set; } = new();


    }
}
