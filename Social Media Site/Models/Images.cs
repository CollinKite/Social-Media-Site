using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Social_Media_Site.Models
{
    public class Images
    {
        public Images() {}
        [Key]
        public int Id { get; set; }

        
        public string? Image { get; set; }
        
        public IdentityUser? User { get; set; }

        public string? UserId { get; set; }
    }
}
