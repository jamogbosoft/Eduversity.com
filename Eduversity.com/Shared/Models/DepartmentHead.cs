using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eduversity.com.Shared.Models
{
    public class DepartmentHead
    {
        [Key]
        public int Id { get; set; }
        public long UserId { get; set; } // Foreign key
        public int LecturerId { get; set; } // Foreign key, Has Unique Index (.HasIndex, .IsUnique)
        public int DepartmentId { get; set; } // Foreign key, Has Unique Index (.HasIndex, .IsUnique)

        [JsonIgnore]

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; } // Reference navigation
        [ForeignKey(nameof(LecturerId))]
        public Lecturer? Lecturer { get; set; } // Reference navigation
        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; } // Reference navigation
    }   
}
