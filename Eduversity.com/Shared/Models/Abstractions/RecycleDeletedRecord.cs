namespace Eduversity.com.Shared.Models.Abstractions
{
    public abstract class RecycleDeletedRecord
    {
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
