using Eduversity.com.Shared.Models.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eduversity.com.Shared.Models
{
    public class Department: AbbreviationRecord
    {
        [Key]
        public int Id { get; set; }
        public int FacultyId { get; set; } // Foreign key  
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]

        [ForeignKey(nameof(FacultyId))]
        public Faculty? Faculty { get; set; } // Reference navigation
        public List<DepartmentOption> Options { get; set; } = new();
    }

}
