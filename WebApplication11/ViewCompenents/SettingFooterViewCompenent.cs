using Microsoft.AspNetCore.Mvc;
using WebApplication11.Data;

namespace WebApplication11.ViewCompenents
{
    public class SettingFooterViewComponent : ViewComponent
    {   
        private readonly FiorelloDbContext _context;
        public SettingFooterViewComponent(FiorelloDbContext context)
        {
            _context = context;
            
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           var settings=_context.settings.ToDictionary(Key=>Key.Key, val=>val.Value);
            return View(await Task.FromResult(settings));
        }


    }
}
