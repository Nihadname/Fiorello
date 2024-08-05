using System.ComponentModel.DataAnnotations;

namespace WebApplication11.ViewModels
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int Count { get; set; }

        public IFormFile[] Photos { get; set; }

    }
}
