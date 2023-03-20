using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduversity.com.Shared.Models
{
    public class LecturerAcademicDetail
    {
        public int LecturerId { get; set; } // Foreign key  
        public int DepartmentId { get; set; } // Foreign key  
        [MaxLength(70)]
        public string Designation { get; set; } = string.Empty;
        public string Specialisation { get; set; } = string.Empty;
        public string Certifications { get; set; } = string.Empty;
        public string Activities { get; set; } = string.Empty;
        public string ProfessionalAffiliation { get; set; } = string.Empty;
        public string Award { get; set; } = string.Empty;
        public string Memo { get; set; } = string.Empty;
        public List<Qualification> Qualifications { get; set; } = new();

        [ForeignKey(nameof(LecturerId))]
        public Lecturer? Lecturer { get; set; } // Reference navigation       

        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; } // Reference navigation
    }
}
