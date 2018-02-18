using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LmycDataLib.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Users : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Users> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string MobileNumber { get; set; }
        public string Experience { get; set; }
    }

    public class Roles : IdentityRole
    {
        public Roles() : base() { }
        public Roles(string roleName) : base(roleName) { }
    }

    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Boat> Boats { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<LmycDataLib.Models.Roles> IdentityRoles { get; set; }

        //public System.Data.Entity.DbSet<Roles> IdentityRoles { get; set; }


        //public System.Data.Entity.DbSet<LmycDataLib.Models.ApplicationUser> ApplicationUsers { get; set; }


        //public System.Data.Entity.DbSet<LmycDataLib.Models.RoleViewModel> RoleViewModels { get; set; }
    }
}