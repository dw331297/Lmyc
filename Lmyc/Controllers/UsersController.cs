using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LmycDataLib.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lmyc.Controllers
{
    [Authorize(Roles="Admin")]
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult MainPage()
        {
            return View();
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Role/Create
        public ActionResult AddRoleToUser(string id)
        {
            List<string> rnames = new List<string>();
            List<IdentityRole> roles = db.Roles.ToList();

            for(int i = 0; i < roles.Count(); i++)
            {
                rnames.Add(roles[i].Name);
            }

            ViewBag.roles = rnames;

            AddRoleToUser viewModel = new AddRoleToUser()
            {
                UserID = id,
                RoleName = null
            };
            return View(viewModel);
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoleToUser([Bind(Include = "UserId, RoleName")] AddRoleToUser viewModel)
        {
            var uId = viewModel.UserID;
            var rName = viewModel.RoleName;
            if (ModelState.IsValid)
            {
                var userManager = new UserManager<Users>(new UserStore<Users>(db));

                userManager.AddToRole(uId, rName);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = viewModel.UserID });
            }

            return View("Index");
        }

        public ActionResult RemoveRoleFromUser(string id)
        {
            var userManager = new UserManager<Users>(new UserStore<Users>(db));

            List<string> rnames = new List<string>();
            List<IdentityRole> roles = db.Roles.ToList();

            for (int i = 0; i < roles.Count(); i++)
            {
                if(userManager.IsInRole(id, roles[i].Name))
                {
                    rnames.Add(roles[i].Name);
                }
            }

            ViewBag.roles = rnames;

            AddRoleToUser viewModel = new AddRoleToUser()
            {
                UserID = id,
                RoleName = null
            };
            return View(viewModel);
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveRoleFromUser([Bind(Include = "UserId, RoleName")] AddRoleToUser viewModel)
        {
            var uId = viewModel.UserID;
            var rName = viewModel.RoleName;
            if (ModelState.IsValid)
            {
                var userManager = new UserManager<Users>(new UserStore<Users>(db));

                userManager.RemoveFromRole(uId, rName);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = viewModel.UserID });
            }

            return View("Index");
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Street,City,Province,PostalCode,Country,MobileNumber,Experience,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Roles")] Users users)
        {
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("P@$$w0rd");
            if (ModelState.IsValid)
            {
                users.PasswordHash = password;
                users.SecurityStamp = Guid.NewGuid().ToString();
                db.Users.Add(users);
                db.SaveChanges();

                var userManager = new UserManager<Users>(new UserStore<Users>(new ApplicationDbContext()));
                userManager.AddToRole(userManager.FindByEmail(users.Email).Id, "Member");

                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Street,City,Province,PostalCode,Country,MobileNumber,Experience,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
