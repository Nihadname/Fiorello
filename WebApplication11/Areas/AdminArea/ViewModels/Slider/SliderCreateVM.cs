using System.ComponentModel.DataAnnotations;

namespace WebApplication11.Areas.AdminArea.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile sliderImage {  get; set; }
    }
}
