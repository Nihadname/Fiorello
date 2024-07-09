using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication11.ViewModels
{
    public class BasketVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int BasketCount { get; set; }
        public string IMageName { get; set; }
    }
}
