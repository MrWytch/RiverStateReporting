using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverStateReporting.Data.Model;
using System.Threading.Tasks;

namespace RiverStateReporting.Pages.Account
{
    /// <summary>
    /// Just a logic for logout procedure. No actual page. After logout redirects to home page. 
    /// </summary>
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            returnUrl = returnUrl ?? "/Index";
            return LocalRedirect(returnUrl);
        }
    }
}
