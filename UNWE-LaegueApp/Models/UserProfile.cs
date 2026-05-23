using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace UNWE_LaegueApp.Models
{
    public class UserProfile
    {
        [Key]
        public string UserId { get; set; } = null!;

        [ForeignKey("UserId")] 
        public IdentityUser User { get; set; } = null!;

        [Required(ErrorMessage = "Името е задължително.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Презимето е задължително.")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилията е задължителна.")]
        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {MiddleName} {LastName}";
    }
}