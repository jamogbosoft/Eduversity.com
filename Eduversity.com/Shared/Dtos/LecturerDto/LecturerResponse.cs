using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.LecturerDto
{
    public class LecturerResponse
    {
        public int Id { get; set; }
        public long UserId { get; set; }

        [Required, MaxLength(45, ErrorMessage = "Name should not exceed 45 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(1, ErrorMessage = "Gender should not exceed 1 character.")]
        public string Gender { get; set; } = string.Empty;

        [Required, MaxLength(1, ErrorMessage = "Marital Status should not exceed 1 character.")]
        public string MaritalStatus { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public LecturerAcademicDetailResponse AcademicDetail { get; set; } = new();     
    }
}
