using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverStateReporting.Data;
using RiverStateReporting.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiverStateReporting.Pages.Stations
{
    /// <summary>
    /// Page model for listing all the stations in DB showing their most importatn properties. Also inactive stations are highlighted.
    /// </summary>
    [Authorize(Roles = "Administrator, User")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<StationDetails> Stations { get; set; }

        public async Task OnGetAsync()
        {
            var currentDateTime = DateTime.Now;
            Stations = await _context.Stations
                .Select(station => new StationDetails
                {
                    Id = station.Id,
                    River = station.River,
                    RiverKm = station.RiverKm,
                    Title = station.Title,
                    FloodLevel = station.FloodLevel,
                    DroughtLevel = station.DroughtLevel,
                    TimeOutInMinutes = station.TimeOutInMinutes,
                    // Setting flag indicating whether the station is alive (depending on actual time, last value timestamp and Timeout property)
                    IsActive = _context.Values
                        .Where(v => v.StationId == station.Id)
                        .OrderByDescending(v => v.TimeStamp)
                        .Any(v => v.TimeStamp >= currentDateTime.AddMinutes(-station.TimeOutInMinutes))
                })
                .ToListAsync();
        }

        public class StationDetails
        {
            public int Id { get; set; }
            public string River { get; set; }
            public double RiverKm { get; set; }
            public string Title { get; set; }
            public int FloodLevel { get; set; }
            public int DroughtLevel { get; set; }
            public int TimeOutInMinutes { get; set; }
            public bool IsActive { get; set; }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var station = await _context.Stations.FindAsync(id);
            if (station != null)
            {
                _context.Stations.Remove(station);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}


