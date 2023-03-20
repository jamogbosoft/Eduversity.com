using Eduversity.com.Shared.Models.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eduversity.com.Shared.Models
{
    public class LGA: EditableRecord
    {
        [Key]
        public int Id { get; set; }
        public int StateId { get; set; } // Foreign key
        [Required]
        [StringLength(30)]
        public string Name { get; set; } = string.Empty;


        [JsonIgnore]

        [ForeignKey(nameof(StateId))]
        public State? State { get; set; } // Reference navigation
    }
}
