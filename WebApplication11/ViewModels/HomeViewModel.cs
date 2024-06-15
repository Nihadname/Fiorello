using WebApplication11.Models;

namespace WebApplication11.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SliderContent Content { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> products { get; set; }
        public About About { get; set; }
        public Expert Expert { get; set; }
    }
}
