using LmycDataLib.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lmyc.Data
{
    public class DummyData
    {
        public static List<ApplicationUser> GetUsers()
        {
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("P@$$w0rd");
            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "4f4b5137-2875-4eea-a63d-100af576e643",
                    UserName = "a",
                    PasswordHash = password,
                    Email = "a@a.com",
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser()
                {
                    Id = "88b619a9-9818-4369-9000-8c1809ccc482",
                    UserName = "m",
                    PasswordHash = password,
                    Email = "m@m.com",
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            };
            return users;
        }

        public static List<Boat> GetBoats(ApplicationDbContext context)
        {
            List<Boat> boats = new List<Boat>()
            {
                new Boat()
                {
                    BoatName = "OceanPearl",
                    LengthInFeet = 22,
                    Year = 1987,
                    RecordCreationDate = new DateTime(2013, 10, 5),
                    //CreatedBy = context.Users.Find("4f4b5137-2875-4eea-a63d-100af576e643").Id
                },
                new Boat()
                {
                    BoatName = "USS Saratoga",
                    LengthInFeet = 56,
                    Year = 1998,
                    RecordCreationDate = new DateTime(2011, 3, 4),
                    //CreatedBy = context.Users.Find("3a086999-8685-4169-abae-cf29e378dfe5").Id
                }
            };
            return boats;
        }
    }
}