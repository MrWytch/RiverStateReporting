using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RiverStateReporting.Data;

namespace RiverStateReporting.Services
{
    /// <summary>
    /// Background service to regurarly check flood status of each station and then use email service to send warning
    /// </summary>
    public class FloodMonitoringService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IEmailSender _emailSender;

        public FloodMonitoringService(IServiceScopeFactory scopeFactory, IEmailSender emailSender)
        {
            _scopeFactory = scopeFactory;
            _emailSender = emailSender;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await MonitorStations();
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Flood status check initiated every X minutes
            }
        }

        private async Task MonitorStations()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var stations = await dbContext.Stations.Include(s => s.Values).ToListAsync();

                foreach (var station in stations)
                {
                    var latestValue = station.Values.OrderByDescending(v => v.TimeStamp).FirstOrDefault();
                    if (latestValue != null && latestValue.Val > station.FloodLevel)
                    {
                        // Warning e-mail will be sent only once for each flood level exceeding
                        if (!station.WarningSent)
                        {
                            // Send e-mail
                            if (!station.AlertEmail1.IsNullOrEmpty())
                            {
                                await _emailSender.SendEmailAsync(station.AlertEmail1, "Flood warning", "Your station " + station.Title + " is above the flood level.");
                            }
                            if (!station.AlertEmail2.IsNullOrEmpty())
                            {
                                await _emailSender.SendEmailAsync(station.AlertEmail1, "Flood warning", "Your station " + station.Title + " is above the flood level.");
                            }
                            if (!station.AlertEmail3.IsNullOrEmpty())
                            {
                                await _emailSender.SendEmailAsync(station.AlertEmail1, "Flood warning", "Your station " + station.Title + " is above the flood level.");
                            }

                            station.WarningSent = true; // Flag meaning that for this flood instance warning email was sent.
                            dbContext.Update(station);
                        }
                    }
                    else
                    {
                        station.WarningSent = false; // Warning flag resets when water level is normal again 
                        dbContext.Update(station);
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
