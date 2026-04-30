namespace BanHangOnline.Migrations
{
    using BanHangOnline.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration
        : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

            var userManager =
                new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

            // Role Admin
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            // Role Customer
            if (!roleManager.RoleExists("Customer"))
            {
                roleManager.Create(new IdentityRole("Customer"));
            }

            // Create admin account
            if (!context.Users.Any(x => x.Email == "admin@gmail.com"))
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FullName = "Administrator",
                    Phone = "0123456789"
                };

                var result = userManager.Create(admin, "Admin@123");

                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, "Admin");
                }
            }
        }
    }
}