using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgriEnergyConnect.Models;

namespace AgriEnergyConnect.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Ensure the roles exist
            string[] roleNames = { "Farmer", "Employee" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create default Farmer user
            var farmerUser = new ApplicationUser
            {
                UserName = "farmer@example.com",
                Email = "farmer@example.com"
            };
            if (userManager.Users.All(u => u.UserName != farmerUser.UserName))
            {
                var result = await userManager.CreateAsync(farmerUser, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(farmerUser, "Farmer");
                }
            }

            // Create default Employee user
            var employeeUser = new ApplicationUser
            {
                UserName = "employee@example.com",
                Email = "employee@example.com"
            };
            if (userManager.Users.All(u => u.UserName != employeeUser.UserName))
            {
                var result = await userManager.CreateAsync(employeeUser, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(employeeUser, "Employee");
                }
            }
        }
    }
}
