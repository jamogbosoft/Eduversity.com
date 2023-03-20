using Eduversity.com.Shared.Models.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eduversity.com.Shared.Models
{
    public class DepartmentOption: AbbreviationRecord
    {
        [Key]
        public int Id { get; set; }
        public int DepartmentId { get; set; } // Foreign key
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]

        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; } // Reference navigation
    }
}
