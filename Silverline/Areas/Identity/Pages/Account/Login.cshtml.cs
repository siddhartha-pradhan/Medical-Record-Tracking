using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.ToastNotification.Abstractions;
using Silverline.Application.Interfaces.Services;

namespace Silverline.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
		private readonly INotyfService _notyf;
        private readonly ILogger<LoginModel> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

		public LoginModel(SignInManager<IdentityUser> signInManager,
            IPatientService patientService,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager,
			INotyfService notyf)
        {
            _notyf = notyf;
			_logger = logger;
            _signInManager = signInManager;
			_userManager = userManager;
		}

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string? state, string? message, string returnUrl = null)
        {
            if(state != null)
            {
				switch (state)
				{
					case "Error":
						_notyf.Error("Your account has been locked due to suspicious actions.");
						break;
					case "Invalid":
						_notyf.Error("Invalid email address or password, try again.");
						break;
					case "Warning":
						_notyf.Warning("Your account has not been confirmed yet.");
						break;
				}
			}

			if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

				if (result.RequiresTwoFactor)
				{
					return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
				}

				if (result.IsLockedOut)
				{
					return RedirectToPage("Login", new { state = "Error", returnUrl = returnUrl });
				}

				if (result.IsNotAllowed)
                {
					return RedirectToPage("Login", new { state = "Warning", returnUrl = returnUrl });
				}

				if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

					var user = await _userManager.FindByNameAsync(Input.Email);

					var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains(Constants.Admin))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
					if (roles.Contains(Constants.Patient))
					{
						return RedirectToAction("Index", "Home", new { area = "Patient" });
					}
					if (roles.Contains(Constants.Doctor))
					{
						return RedirectToAction("Index", "Home", new { area = "Doctor" });
					}
					if (roles.Contains(Constants.Pharmacist))
					{
						return RedirectToAction("Index", "Home", new { area = "Pharmacist" });
					}
					if (roles.Contains(Constants.LabTechnician))
					{
						return RedirectToAction("Index", "Home", new { area = "LabTechnician" });
					}

					return LocalRedirect(returnUrl);
                }
                
                else
                {
					return RedirectToPage("Login", new { state = "Invalid", returnUrl = returnUrl });
				}
            }

            return Page();
        }
    }
}
