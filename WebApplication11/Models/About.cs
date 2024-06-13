using WebApplication11.Models.Common;

namespace WebApplication11.Models
{
    public class About: BaseEntity
    {
        public string Title {  get; set; } 
        public string Description { get; set; }
        public string ImageUrl { get; set; }

    }
}
