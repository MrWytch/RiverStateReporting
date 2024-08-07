using Microsoft.AspNetCore.Identity;

namespace RiverStateReporting.Data
{
    /// <summary>
    /// Initializes roles
    /// </summary>
    public class RoleInitializer
    {
        public static async Task RolesInitAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = new string[] { "Administrator", "User", "Guest" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
