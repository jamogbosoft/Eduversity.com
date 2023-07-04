using Eduversity.com.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.StudentDto
{
    public class StudentResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        [Required, MaxLength(20, ErrorMessage = "First name should not exceed 20 characters.")]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(20, ErrorMessage = "Middle name should not exceed 20 characters.")]
        public string MiddleName { get; set; } = string.Empty;
        [Required, MaxLength(25, ErrorMessage = "Last name should not exceed 25 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [MaxLength(150, ErrorMessage = "Place of Birth should not exceed 150 characters.")]       
        [Display(Name = "Place of Birth")]
        public string PlaceOfBirth { get; set; } = string.Empty;
        [Required, MaxLength(1, ErrorMessage = "Gender should not exceed 1 character.")]
        public string Gender { get; set; } = string.Empty;
        [Required, MaxLength(1, ErrorMessage = "Marital status should not exceed 1 character.")]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

        public StudentAcademicDetailResponse AcademicDetail { get; set; } = new();
    }
}
