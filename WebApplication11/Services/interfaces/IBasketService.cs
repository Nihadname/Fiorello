using WebApplication11.ViewModels;

namespace WebApplication11.Services.interfaces
{
    public interface IBasketService
    {
         int GetBasketCount();
        decimal GetTotalPrice();
        List<BasketVM> GetBasketList();
    }
}
