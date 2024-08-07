using RiverStateReporting.Data.Model;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace RiverStateReporting.Data
{
    /// <summary>
    /// Seeds data to DB if empty
    /// </summary>
    //public class DbInitializer
    //{
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly SignInManager<ApplicationUser> _signInManager;
    //    private readonly RoleManager<IdentityRole> _roleManager;

    //    public DbInitializer(
    //    UserManager<ApplicationUser> userManager,
    //    SignInManager<ApplicationUser> signInManager,
    //    RoleManager<IdentityRole> roleManager)
    //    {
    //        _userManager = userManager;
    //        _signInManager = signInManager;
    //        _roleManager = roleManager;
    //    }

    //    public static void Initialize(IServiceProvider serviceProvider)
    //    {
    //        using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
    //        if (context.Stations.Any())
    //        {
    //            return;   // DB has been seeded
    //        }

    //        List<Station> stations = new List<Station>();

    //        stations.Add(new Station{
    //            River = "Mississippi",
    //            RiverKm = 10.3,
    //            Title = "Mississippi 10,3",
    //            FloodLevel = 100,
    //            DroughtLevel = 40,
    //            TimeOutInMinutes = 180,
    //            AlertEmail1 = "alert1@example.com",
    //            AlertEmail2 = "alert2@example.com",
    //            AlertEmail3 = "alert3@example.com",
    //            CreatedOn = DateTime.Now,
    //            CreatedByUser = "admin"
    //        });

    //        stations.Add(new Station
    //        {
    //            River = "Mississippi",
    //            RiverKm = 120.8,
    //            Title = "Mississippi 120,8",
    //            FloodLevel = 500,
    //            DroughtLevel = 100,
    //            TimeOutInMinutes = 180,
    //            AlertEmail1 = "alert1@example.com",
    //            AlertEmail2 = "alert2@example.com",
    //            AlertEmail3 = "alert3@example.com",
    //            CreatedOn = DateTime.Now,
    //            CreatedByUser = "admin"
    //        });

    //        stations.Add(new Station
    //        {
    //            River = "Hudson",
    //            RiverKm = 20.9,
    //            Title = "Hudson 20,9",
    //            FloodLevel = 90,
    //            DroughtLevel = 30,
    //            TimeOutInMinutes = 180,
    //            AlertEmail1 = "alert1@example.com",
    //            AlertEmail2 = "alert2@example.com",
    //            AlertEmail3 = "alert3@example.com",
    //            CreatedOn = DateTime.Now,
    //            CreatedByUser = "admin"
    //        });

    //        stations.Add(new Station
    //        {
    //            River = "Hudson",
    //            RiverKm = 250.1,
    //            Title = "Hudson 250,1",
    //            FloodLevel = 600,
    //            DroughtLevel = 150,
    //            TimeOutInMinutes = 180,
    //            AlertEmail1 = "alert1@example.com",
    //            AlertEmail2 = "alert2@example.com",
    //            AlertEmail3 = "alert3@example.com",
    //            CreatedOn = DateTime.Now,
    //            CreatedByUser = "admin"
    //        });

    //        foreach (var station in stations)
    //        {
    //            context.Stations.Add(station);
    //        }

            
    //        context.SaveChanges();
    //    }
    //}
}
