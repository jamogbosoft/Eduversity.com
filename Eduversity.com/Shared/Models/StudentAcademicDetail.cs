using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduversity.com.Shared.Models
{
    public  class StudentAcademicDetail
    {
        public long StudentId { get; set; } // Foreign key  
        public int DepartmentOptionId { get; set; }
        [Required]
        [MaxLength(20)]
        public string RegNumber { get; set; } = string.Empty;
        [Required]
        [MaxLength(9)]
        public string Session { get; set; } = string.Empty;
        [Required]
        public int Level { get; set; }
        [Required]
        [MaxLength(6)]
        public string Semester { get; set; } = string.Empty;
        public bool Graduated { get; set; } = false;
        public bool PassedOut { get; set; } = false;
        public string Memo { get; set; } = string.Empty;

        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; } // Reference navigation

        [ForeignKey(nameof(DepartmentOptionId))]
        public DepartmentOption? DepartmentOption { get; set; }
    }
}
