using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverStateReporting.Data;
using RiverStateReporting.Data.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RiverStateReporting.Pages
{
    /// <summary>
    /// Page model for the home page. Includes checks of existing items in the DB. If the DB is empty, the page promts the user to do certain actions.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public bool NoAdministrator { get; set; }
        public bool NoStations { get; set; }

        public async Task OnGetAsync()
        {
            // Check if there is any administrator
            var adminRoleExists = await _roleManager.RoleExistsAsync("Administrator");
            if (adminRoleExists)
            {
                var admins = await _userManager.GetUsersInRoleAsync("Administrator");
                NoAdministrator = admins.Count == 0;
            }
            else
            {
                NoAdministrator = true;
            }

            // Check if there is any station in DB
            NoStations = !_context.Stations.Any();
        }

        public async Task<IActionResult> OnPostSeedDatabaseAsync()
        {
            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            if (!_context.Stations.Any())
            {
                // Seeding stations
                var stations = new[]
                {
                    new Station
                    {
                        River = "Mississippi",
                        RiverKm = 10.3,
                        Title = "Mississippi 10,3",
                        FloodLevel = 100,
                        DroughtLevel = 40,
                        TimeOutInMinutes = 180,
                        AlertEmail1 = "alert1@example.com",
                        AlertEmail2 = "alert2@example.com",
                        AlertEmail3 = "alert3@example.com",
                        CreatedOn = DateTime.Now,
                        CreatedByUser = User.Identity.Name
                    },
                    new Station
                    {
                        River = "Mississippi",
                        RiverKm = 120.8,
                        Title = "Mississippi 120,8",
                        FloodLevel = 500,
                        DroughtLevel = 100,
                        TimeOutInMinutes = 180,
                        AlertEmail1 = "alert1@example.com",
                        AlertEmail2 = "alert2@example.com",
                        AlertEmail3 = "alert3@example.com",
                        CreatedOn = DateTime.Now,
                        CreatedByUser = User.Identity.Name
                    },
                    new Station
                    {
                        River = "Hudson",
                        RiverKm = 20.9,
                        Title = "Hudson 20,9",
                        FloodLevel = 90,
                        DroughtLevel = 30,
                        TimeOutInMinutes = 180,
                        AlertEmail1 = "alert1@example.com",
                        AlertEmail2 = "alert2@example.com",
                        AlertEmail3 = "alert3@example.com",
                        CreatedOn = DateTime.Now,
                        CreatedByUser = User.Identity.Name
                    },
                    new Station
                    {
                        River = "Hudson",
                        RiverKm = 250.1,
                        Title = "Hudson 250,1",
                        FloodLevel = 600,
                        DroughtLevel = 150,
                        TimeOutInMinutes = 180,
                        AlertEmail1 = "alert1@example.com",
                        AlertEmail2 = "alert2@example.com",
                        AlertEmail3 = "alert3@example.com",
                        CreatedOn = DateTime.Now,
                        CreatedByUser = User.Identity.Name
                    }
                };

                _context.Stations.AddRange(stations);
                await _context.SaveChangesAsync();

                // Seeding values for each station
                foreach (var station in stations)
                {
                    var values = new[]
                    {
                        new Value
                        {
                            StationId = station.Id,
                            Val = new Random().Next(30, 150),
                            TimeStamp = DateTime.Now.AddDays(-1)
                        },
                        new Value
                        {
                            StationId = station.Id,
                            Val = new Random().Next(30, 150),
                            TimeStamp = DateTime.Now.AddHours(-12)
                        },
                        new Value
                        {
                            StationId = station.Id,
                            Val = new Random().Next(30, 150),
                            TimeStamp = DateTime.Now.AddHours(-6)
                        }
                    };

                    _context.Values.AddRange(values);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
