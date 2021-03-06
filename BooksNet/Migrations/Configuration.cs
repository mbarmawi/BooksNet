namespace BooksNet.Migrations
{
  using BooksNet.Areas.Admin.Models;
  using BooksNet.Models;
  using Microsoft.AspNet.Identity;
  using Microsoft.AspNet.Identity.EntityFramework;
  using System;
  using System.Data.Entity.Migrations;
  using System.Linq;

  internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(ApplicationDbContext context)
    {
      // create roles
      var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

      string[] roles = new string[] { Roles.Admin, Roles.SubAdmin, Roles.Staff };

      foreach (string role in roles)
      {
        if (!roleManager.RoleExists(role))
        {
          roleManager.Create(new IdentityRole(role));
        }
      }

      // Create default admin user
      if (!context.Users.Any())
      {
        ApplicationUser admin = new ApplicationUser()
        {
          Email = "admin@booksnet.com",
          UserName = "admin@booksnet.com",
          FirstName = "admin",
          LastName = "admin",
          CreateDate = DateTime.Now,
          LastUpdate = DateTime.Now
        };

        var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        var result = userManager.Create(admin, "Asd!23");
        if (result.Succeeded)
        {
          userManager.AddToRole(admin.Id, Roles.Admin);
        }
      }

      // Add default profile 
      if (!context.Profile.Any())
      {
        context.Profile.Add(new Profile()
        {
          Name= "Books Net",
          Description= "Online library management system.",
          Title="BooksNet"
        });
        context.SaveChanges();
      }
    }
  }
}