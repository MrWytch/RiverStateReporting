using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverStateReporting.Data;
using RiverStateReporting.Data.Model;

namespace RiverStateReporting.Pages.Stations
{
    /// <summary>
    /// Page model for form and procedure of adding new stations to the DB.
    /// </summary>
    [Authorize(Roles = "Administrator, User")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Station Station { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Station.CreatedOn = DateTime.Now;
            Station.CreatedByUser = User.Identity.Name.ToString();

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                TempData["Errors"] = string.Join(", ", errorMessages);

                return Page();
            }

            _context.Stations.Add(Station);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

