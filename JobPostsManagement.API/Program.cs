using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobPostsManagement.API.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DbContext = JobPostsManagement.API.Data.DbContext;

namespace JobPostsManagement.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                await SeedData(serviceScope);

                var configuration = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();

            }
            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static async Task SeedData(IServiceScope serviceScope)
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<DbContext>();

            await dbContext.Database.MigrateAsync();

            //Generate user roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync("Employer"))
            {
                var userRole = new IdentityRole("Employer");
                await roleManager.CreateAsync(userRole);
            }

            if (!await roleManager.RoleExistsAsync("Candidate"))
            {
                var userRole = new IdentityRole("Candidate");
                await roleManager.CreateAsync(userRole);
            }

            //add users to test auth
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Models.BaseUser>>();

            if (await userManager.FindByEmailAsync("admin@test.com") == null)
            {
                var employee = new Models.BaseUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                    FirstName = "Admin"
                };
                await userManager.CreateAsync(employee, "P@ssw0rd");
                await userManager.AddToRoleAsync(employee, "Admin");
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
