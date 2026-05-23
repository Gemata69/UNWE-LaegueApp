using System.ComponentModel.DataAnnotations;

namespace UNWE_LaegueApp.Models
{
    public class JoinRequest
    {
        public int Id { get; set; }

        public string UserEmail { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string UserFullName { get; set; } = string.Empty;

        public int TeamId { get; set; }

        public Team? Team { get; set; } = default!;

        public string Status { get; set; } = "Pending";
    }
}