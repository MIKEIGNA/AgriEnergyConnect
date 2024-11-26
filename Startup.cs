using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AgriEnergyConnect.Data;
using Microsoft.AspNetCore.Identity;
using AgriEnergyConnect.Models;
using AgriEnergyConnect.ViewModels;
using System;

namespace AgriEnergyConnect
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();

            // Add Razor Pages service
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "loginRoute",
                    pattern: "Home/{action=Login}/{id?}",
                    defaults: new { controller = "Home", action = "Login" });

                endpoints.MapControllerRoute(
                    name: "addFarmerRoute",
                    pattern: "Employee/{action=AddFarmer}/{id?}",
                    defaults: new { controller = "Employee", action = "AddFarmer" });

                endpoints.MapControllerRoute(
                    name: "viewFarmersRoute",
                    pattern: "Employee/{action=ViewFarmers}/{id?}",
                    defaults: new { controller = "Employee", action = "ViewFarmers" });

                endpoints.MapControllerRoute(
                    name: "farmerIndexRoute",
                    pattern: "Farmer/{action=Index}/{id?}",
                    defaults: new { controller = "Farmer", action = "Index" });

                endpoints.MapRazorPages();
            });

            // Seed the database with default users
            SeedData.Initialize(serviceProvider).Wait();
        }
    }
}
