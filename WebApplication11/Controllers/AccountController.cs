using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using WebApplication11.Models;
using WebApplication11.Services.interfaces;
using WebApplication11.ViewModels;

namespace WebApplication11.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            AppUser appUser = new AppUser
            {
                fullName = registerVM.FullName,
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }

            await _userManager.AddToRoleAsync(appUser, nameof(RolesEnum.Member));
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            string link = Url.Action(nameof(VerifyEmail), "Account", new { email = appUser.Email, token = token }, Request.Scheme, Request.Host.ToString());

            string body;
            using (StreamReader sr = new StreamReader("wwwroot/EmailPage/emailConfirm.html"))
            {
                body = sr.ReadToEnd();
            }
            body = body.Replace("{{link}}", link);
            body = body.Replace("{{UserName}}", appUser.UserName);

            _emailService.SendEmail(
                from: "nihadmi@code.edu.az",
                to: appUser.Email,
                subject: "verify Email",
                body: body,
                smtpHost: "smtp.gmail.com",
                smtpPort: 587,
                enableSsl: true,
                smtpUser: "nihadmi@code.edu.az",
                smtpPass: "ilyo ibry uphi gnfe\r\n"
            );

            return Content("You are registered");
        }
    }
}
