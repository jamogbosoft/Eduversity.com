using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Models.Abstractions
{
    public abstract class AbbreviationRecord : EditableRecord
    {
        [Required]
        [StringLength(3)]
        public string Abbreviation { get; set; } = string.Empty;
    }
}
