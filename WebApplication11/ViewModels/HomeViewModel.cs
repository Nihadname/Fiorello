using WebApplication11.Models;

namespace WebApplication11.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SliderContent Content { get; set; }
    }
}
