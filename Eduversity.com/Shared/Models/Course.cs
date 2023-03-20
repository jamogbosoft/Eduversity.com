using Eduversity.com.Shared.Models.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Eduversity.com.Shared.Models
{
    public class Course: RecycleDeletedRecord
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(7, MinimumLength = 7)]
        //A unique index was defined using fluent API in DataContext.cs
        public string Code { get; set; } = string.Empty;
        [Required, StringLength(75)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [Range(1, 6)]
        public int Unit { get; set; }
        [MaxLength(1)]
        public string Status { get; set; } = string.Empty;

        public List<CourseStructure> CourseStructures { get; set; } = new();
        public List<CourseAllocation> CourseAllocations { get; set; } = new();
    }
}
