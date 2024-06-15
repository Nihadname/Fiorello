using WebApplication11.Models.Common;

namespace WebApplication11.Models
{
    public class Expert:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
       public List<ExpertItem> ExpertItems { get; set; }


    }
}
