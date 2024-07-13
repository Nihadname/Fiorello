using System.ComponentModel.DataAnnotations;

namespace WebApplication11.Areas.AdminArea.ViewModels.Slider
{
    public class SliderUpdateVM
    {
        [Required]
        public IFormFile photo {  get; set; }
        public string ImageUrl  { get; set; }
    }
}
