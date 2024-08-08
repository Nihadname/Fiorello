using Microsoft.AspNetCore.Identity;
using WebApplication11.Models;

namespace WebApplication11.ViewModels
{
    public class RoleDetailVM
    {
        public IdentityRole Role { get; set; }
        public IList<AppUser> Users { get; set; }
    }
}
