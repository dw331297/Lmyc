namespace Lmyc.Migrations.BoatMigrations
{
    using Lmyc.Data;
    using LmycDataLib.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LmycDataLib.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\BoatMigrations";
        }

        protected override void Seed(LmycDataLib.Models.ApplicationDbContext context)
        {
            //context.Users.AddOrUpdate(
            //    u => u.UserName, DummyData.GetUsers().ToArray());
            //context.SaveChanges();
            //var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //string[] roleNames = { "Admin", "Member"};
            //IdentityResult roleResult;
            //foreach (var roleName in roleNames)
            //{
            //    if(!RoleManager.RoleExists(roleName))
            //    {
            //        roleResult = RoleManager.Create(new IdentityRole(roleName));
            //    }
            //}

            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //UserManager.AddToRole("4f4b5137-2875-4eea-a63d-100af576e643", "Admin");
            //UserManager.AddToRole("88b619a9-9818-4369-9000-8c1809ccc482", "Member");

            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}


            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));
            if (!roleManager.RoleExists("Member"))
                roleManager.Create(new IdentityRole("Member"));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (userManager.FindByEmail("a@a.a") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "a@a.a",
                    UserName = "a",
                };
                var result = userManager.Create(user, "P@$$w0rd");
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Admin");
            }
            if (userManager.FindByEmail("m@m.m") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "m@m.m",
                    UserName = "m",
                };
                var result = userManager.Create(user, "P@$$w0rd");
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Member");
            }

            var Auser = userManager.FindByEmail("a@a.a");
            var Muser = userManager.FindByEmail("m@m.m");


            List<Boat> boats = new List<Boat>()
            {
                new Boat()
                {
                    BoatName = "USS Enterprise",
                    LengthInFeet = 1300,
                    Picture = "https://vignette.wikia.nocookie.net/memoryalpha/images/d/df/USS_Enterprise-A_quarter.jpg/revision/latest?cb=20100518022537&path-prefix=en",
                    Year = 2200,
                    Make = "Constitution Class",
                    RecordCreationDate = new DateTime(2013, 10, 5),
                    CreatedBy = Auser.Id
                },
                new Boat()
                {
                    BoatName = "USS Defiant",
                    LengthInFeet = 900,
                    Picture = "http://www.ex-astris-scientia.org/scans/other/defiant-publicity.jpg",
                    Year = 2370,
                    Make = "Defiant Class",
                    RecordCreationDate = new DateTime(2011, 3, 4),
                    CreatedBy = Muser.Id
                }
            };
           

            context.Boats.AddOrUpdate(b => b.BoatName, boats.ToArray());
            context.SaveChanges();

        }
    }
}
