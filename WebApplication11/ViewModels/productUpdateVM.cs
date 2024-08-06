using System.ComponentModel.DataAnnotations;
using WebApplication11.Models;

namespace WebApplication11.ViewModels
{
    public class productUpdateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int Count { get; set; }

        public List<ProductImage>? Images { get; set; }
        public IFormFile[]? Photos { get; set; }
    }
}
