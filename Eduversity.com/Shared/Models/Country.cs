using Eduversity.com.Shared.Models.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Models
{
    public class Country: EditableRecord
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; } = string.Empty;
     
        public List<State> States { get; set; } = new();
    }
}