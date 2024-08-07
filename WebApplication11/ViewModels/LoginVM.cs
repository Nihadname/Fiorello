using System.ComponentModel.DataAnnotations;

namespace WebApplication11.ViewModels
{
    public class LoginVM
    {
        [Required, MaxLength(100)]
        public string EmailOrUserName { get; set; }
        [Required, MaxLength(100), DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me ?")]
        public bool RememberMe { get; set; }
    }
}
