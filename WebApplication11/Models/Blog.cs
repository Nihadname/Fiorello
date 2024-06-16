using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using WebApplication11.Models.Common;

namespace WebApplication11.Models
{
    public class Blog:BaseEntity
    {
        [Required,MaxLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(280)]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
