using WebApplication11.Models.Common;

namespace WebApplication11.Models
{
    public class ProductImage:BaseEntity
    {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public bool IsMain { get; set; }
        public Product Product { get; set; }
    }
}
