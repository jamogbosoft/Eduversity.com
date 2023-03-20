using System.Text.Json.Serialization;

namespace Eduversity.com.Shared.Models
{
    public class UserRole
    {
        public long UserId { get; set; } // Foreign key
        public int RoleId { get; set; } // Foreign key

        [JsonIgnore]
        public User? User { get; set; } // Reference navigation
        public Role? Role { get; set; } // Reference navigation       
    }
}
