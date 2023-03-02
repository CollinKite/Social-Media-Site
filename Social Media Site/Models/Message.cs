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

        public IdentityUser? Sender { get; set; }
        public IdentityUser? Reciver { get; set; }
        
        public string? Content { get; set; }
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }

        public DateTime? Date { get; set; }
    }
}
