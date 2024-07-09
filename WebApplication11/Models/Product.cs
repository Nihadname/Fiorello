using System.ComponentModel.DataAnnotations.Schema;
using WebApplication11.Models.Common;

namespace WebApplication11.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int Count { get; set; }
     
      public List<ProductImage> Images { get; set; }

    }
}
