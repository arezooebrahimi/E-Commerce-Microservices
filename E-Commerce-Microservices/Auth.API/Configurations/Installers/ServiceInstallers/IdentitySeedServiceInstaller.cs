using Auth.API.Configurations.Installers;
using Common.Attributes;
using Common.Entities.Auth;
using Microsoft.AspNetCore.Identity;

namespace Auth.Configurations.Installers.ServiceInstallers
{
    [InstallerOrder(Order = 6)]
    public class IdentitySeedServiceInstaller : IServiceInstaller
    {
        public async Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            // Build temporary service provider to resolve scoped services
            using var serviceProvider = services.BuildServiceProvider();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var logger = serviceProvider.GetRequiredService<ILogger<IdentitySeedServiceInstaller>>();

            // 1. Seed Roles
            string[] roles = new[] { "User", "Admin" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role));
                    if (result.Succeeded)
                        logger.LogInformation($"Role '{role}' created.");
                    else
                        logger.LogError($"Failed to create role '{role}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            // 2. Seed Admin User
            var adminPhone = "09123456789"; // شماره موبایل پیش‌فرض
            var adminUser = await userManager.FindByNameAsync(adminPhone);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminPhone,
                    PhoneNumber = adminPhone,
                    NameFamily = "مدیر سیستم"
                };

                var createResult = await userManager.CreateAsync(adminUser, "Admin@1234"); // پسورد پیش‌فرض

                if (createResult.Succeeded)
                {
                    logger.LogInformation("Admin user created.");

                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    logger.LogInformation("Admin role assigned to admin user.");
                }
                else
                {
                    logger.LogError($"Failed to create admin user: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                logger.LogInformation("Admin user already exists.");
            }
        }
    }
}
