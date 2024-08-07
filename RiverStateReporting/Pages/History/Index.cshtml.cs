using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverStateReporting.Data;
using RiverStateReporting.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiverStateReporting.Pages.History
{
    /// <summary>
    /// Page model for listing all the historical values. User can filter the records. Values out of drought and flood limits are highlighted.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string RiverFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TitleFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public IList<Value> Values { get; set; }

        public List<string> Rivers { get; set; } // Source for drop-down menu

        public List<string> Titles { get; set; } // Source for drop-down menu

        public async Task OnGetAsync()
        {
            // Get drop-down menu data for filter
            Rivers = await _context.Stations
                                   .Select(s => s.River)
                                   .Distinct()
                                   .OrderBy(r => r)
                                   .ToListAsync();

            Titles = await _context.Stations
                                   .Select(s => s.Title)
                                   .Distinct()
                                   .OrderBy(t => t)
                                   .ToListAsync();

            // Get Station reference (Only StationId is in DB)
            var valuesQuery = _context.Values
                                      .Include(v => v.Station)
                                      .AsQueryable();

            if (!string.IsNullOrEmpty(RiverFilter))
            {
                valuesQuery = valuesQuery.Where(v => v.Station.River == RiverFilter);
            }

            if (!string.IsNullOrEmpty(TitleFilter))
            {
                valuesQuery = valuesQuery.Where(v => v.Station.Title == TitleFilter);
            }

            if (StartDate.HasValue)
            {
                valuesQuery = valuesQuery.Where(v => v.TimeStamp >= StartDate.Value);
            }

            if (EndDate.HasValue)
            {
                valuesQuery = valuesQuery.Where(v => v.TimeStamp <= EndDate.Value);
            }

            Values = await valuesQuery.OrderByDescending(v => v.TimeStamp).ToListAsync();
        }
    }
}
