using System.ComponentModel.DataAnnotations.Schema;

namespace Eduversity.com.Shared.Models.Abstractions
{
    public abstract class EditableRecord: RecycleDeletedRecord
    {
        [NotMapped]
        public bool IsEditing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
