using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using RiverStateReporting.Data;
using RiverStateReporting.Data.Model;
using RiverStateReporting.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5096); // HTTP
    options.ListenLocalhost(7280, listenOptions => listenOptions.UseHttps()); // HTTPS
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity framework configuration, password requirements
builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Cookies configuration to keep user signed-in
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(5);
    options.LoginPath = "/Identity/Account/Login";
    options.SlidingExpiration = true;
});

// Services for sending alert e-mails
// builder.Services.AddTransient<IEmailSender, SmtpEmailSender>(); // Uncomment and configure appsettings.json to send e-mails
builder.Services.AddHostedService<FloodMonitoringService>();

builder.Services.AddRazorPages();

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await RoleInitializer.RolesInitAsync(scope.ServiceProvider);
}

// DB initialization - used when debugging, uses DbInitializer class (also not used anymore), I keep it here just in case
    //using (var scope = app.Services.CreateScope())
    //{
    //    var services = scope.ServiceProvider;
    //    try
    //    {
    //        DbInitializer.Initialize(services);
    //    }
    //    catch (Exception ex)
    //    {
    //        var logger = services.GetRequiredService<ILogger<Program>>();
    //        logger.LogError(ex, "An error occurred while seeding the database.");
    //    }
    //}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection(); // Application is debbuged using only HTTP requests
app.UseStaticFiles();

app.UseRouting();

// Utilization of Identity framework
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();


