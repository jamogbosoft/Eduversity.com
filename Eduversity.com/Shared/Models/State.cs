using Eduversity.com.Shared.Models.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eduversity.com.Shared.Models
{
    public class State: EditableRecord
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; } // Foreign key  
        [Required]
        [StringLength(30)]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]

        [ForeignKey(nameof(CountryId))]
        public Country? Country { get; set; } // Reference navigation
        public List<LGA> LGAs { get; set; } = new();
    }
}
