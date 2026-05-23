using Microsoft.AspNetCore.Identity; 

namespace UNWE_LaegueApp.Models
{
    public class Player : CommonEntity
    {
        public string UserId { get; set; } = string.Empty;

        public bool Captain { get; set; }

        public string Name { get; set; } = string.Empty;

        public Team? Team { get; set; }
        public int TeamId { get; set; }
    }
}