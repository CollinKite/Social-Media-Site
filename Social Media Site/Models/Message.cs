using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Social_Media_Site.Models
{
    public class Message
    {
        public Message()
        {
            Date = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        public string? SenderId { get; set; }
        public IdentityUser? Sender { get; set; }
        public string? ReceiverId { get; set; }
        public IdentityUser? Receiver { get; set; }

        [Required]
        public string? Content { get; set; }
   
        public DateTime? Date { get; set; }

        public async Task<string?> GetSenderNameAsync(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(SenderId);
            return user?.UserName;
        }

        public async Task<string?> GetReceiverNameAsync(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(ReceiverId);
            return user?.UserName;
        }

        public string? GetDate()
        {
            //Return Message Date as string in mm/dd/yyyy - hh:mm format
            return Date?.ToString("MM/dd/yyyy - hh:mm");
        }
    }
}
