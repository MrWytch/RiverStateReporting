using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverStateReporting.Data;
using RiverStateReporting.Data.Model;

namespace RiverStateReporting.Pages.Stations
{
    /// <summary>
    /// Page model for form and procedure of editing existing stations in DB.
    /// </summary>
    [Authorize(Roles = "Administrator, User")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Station Station { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Station = await _context.Stations.FindAsync(id);
            if (Station == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var stationToUpdate = await _context.Stations.FindAsync(Station.Id);
            if (stationToUpdate == null)
            {
                return NotFound();
            }

            stationToUpdate.River = Station.River;
            stationToUpdate.RiverKm = Station.RiverKm;
            stationToUpdate.Title = Station.Title;
            stationToUpdate.FloodLevel = Station.FloodLevel;
            stationToUpdate.DroughtLevel = Station.DroughtLevel;
            stationToUpdate.TimeOutInMinutes = Station.TimeOutInMinutes;
            stationToUpdate.AlertEmail1 = Station.AlertEmail1;
            stationToUpdate.AlertEmail2 = Station.AlertEmail2;
            stationToUpdate.AlertEmail3 = Station.AlertEmail3;
            stationToUpdate.CreatedOn = DateTime.Now;
            stationToUpdate.CreatedByUser = User.Identity.Name;

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
