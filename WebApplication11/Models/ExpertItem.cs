using WebApplication11.Models.Common;

namespace WebApplication11.Models
{
    public class ExpertItem:BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string position { get; set; }
        public int ExpertId { get; set; }
        public Expert Expert { get; set; }
    }
}
