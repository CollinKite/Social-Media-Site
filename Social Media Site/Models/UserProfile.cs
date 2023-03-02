using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Social_Media_Site.Models
{
    public class UserProfile
    {
        public UserProfile(){}
        [Key]
        public int Id { get; set;}

        public IdentityUser? User { get; set; }

        public string? UserId { get; set; }

        
        public string? Name { get; set; }
        
        public string? Image { get; set; }
       
        
        public string? Likes { get; set; }

        
        public string? Dislikes { get; set; }

        
        public int? Age { get; set; }
    }
}
