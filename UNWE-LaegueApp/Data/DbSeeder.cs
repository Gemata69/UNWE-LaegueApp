using Microsoft.AspNetCore.Identity;

namespace UNWE_LaegueApp.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { "User", "Player", "Captain", "Admin" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = "admin@mail.com";
            var adminPassword = "AdminPassword123!"; 

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true 
                };

                var createResult = await userManager.CreateAsync(adminUser, adminPassword);

                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            var gemataUser = await userManager.FindByEmailAsync("gemata@mail.com");
            if (gemataUser != null)
            {
                if (!await userManager.IsInRoleAsync(gemataUser, "Captain"))
                {
                    await userManager.AddToRoleAsync(gemataUser, "Captain");
                }
            }
        }
    }
}