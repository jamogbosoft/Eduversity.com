using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Eduversity.com.Shared.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; } = string.Empty;
    }
}
