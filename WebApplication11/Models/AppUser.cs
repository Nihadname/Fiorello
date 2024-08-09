using Microsoft.AspNetCore.Identity;

namespace WebApplication11.Models
{
    public class AppUser: IdentityUser
    {
        public string fullName { get; set; }
        public bool IsBlocked { get; set; }
        public string? ConnectionId { get; set; }


    }
}
