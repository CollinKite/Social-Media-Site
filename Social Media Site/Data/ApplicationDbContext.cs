using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Social_Media_Site.Models;

namespace Social_Media_Site.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserProfile> Userprofiles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Images> Images { get; set; }
        
        
    }
}