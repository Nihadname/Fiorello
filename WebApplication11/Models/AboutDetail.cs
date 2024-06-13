using System.ComponentModel.DataAnnotations;
using WebApplication11.Models.Common;

namespace WebApplication11.Models
{
    public class AboutDetail: BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
