using Eduversity.com.Shared.Models.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Models
{
    public class Faculty: AbbreviationRecord
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public List<Department> Departments { get; set; } = new();

    }
}
