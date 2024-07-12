using System.ComponentModel.DataAnnotations;

namespace WebApplication11.Areas.AdminArea.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        [MaxLength(60)] 
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
