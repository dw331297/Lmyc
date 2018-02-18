using LmycDataLib.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lmyc.Controllers
{
    [Authorize(Roles="Admin")]
    public class AddRoleToUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AddRoleToUser
        public ActionResult Index(string id)
        {
            ViewBag.roles = db.Roles.ToList();
            AddRoleToUser viewModel = new AddRoleToUser()
            {
                UserID = id,
                RoleName = null
            };
            return View(viewModel);
        }

        // GET: Role/Create
        public ActionResult AddRoleToUser(string id)
        {
            ViewBag.roles = db.Roles.ToList();
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
                return RedirectToAction("EditUser", new { id = viewModel.UserID });
            }

            return View();
        }
    }
}