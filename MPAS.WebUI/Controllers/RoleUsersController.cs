using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MPAS.Domain.Context;
using MPAS.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MPAS.WebUI.Controllers
{
    public class RoleUsersController : Controller
    {
        private EFDbContext db = new EFDbContext();



        // GET: RoleUsers
        public ActionResult Index()
        {
            return View(db.RoleUsers.ToList());
        }

        // GET: RoleUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleUser roleUser = db.RoleUsers.Find(id);
            if (roleUser == null)
            {
                return HttpNotFound();
            }
            return View(roleUser);
        }

        // GET: RoleUsers/Create
        public ActionResult Create()
        {
            var userTemp = db.Users.ToList();
            var userList = new List<string>();
            var roleTemp = db.Roles.ToList();
            var roleList = new List<string>();

            foreach (var i in userTemp)
            {
                userList.Add(i.UserName);
            }

            foreach (var i in roleTemp)
            {
                roleList.Add(i.Name);
            }

            SelectList ulist = new SelectList(userList, "Id");
            SelectList rlist = new SelectList(roleList, "Id");
            ViewBag.fruitlist = ulist;
            ViewBag.roleslist = rlist;

            return View();
        }

        // POST: RoleUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Role")] RoleUser roleUser)
        {
            ///////////////////////////////////////
            var users = db.Users.ToList();
            User selectedUser = null;
            IdentityRole selectedRole = null;


            foreach (var user in users)
            {
                if (user.UserName == roleUser.UserId)
                {
                    selectedUser = user;
                    break;
                }
            }

            foreach (var role in db.Roles.ToList())
            {
                if (role.Name == roleUser.Role)
                {
                    selectedRole = role;
                    break;
                }
            }


            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(db));


            if (!userManager.GetRoles(selectedUser.Id).Contains(selectedRole.Name))
            {
                userManager.AddToRole(selectedUser.Id, selectedRole.Name);
                TempData["notice"] = "Successfully registered";
            }


            return RedirectToAction("Create");
        }

        // GET: RoleUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleUser roleUser = db.RoleUsers.Find(id);
            if (roleUser == null)
            {
                return HttpNotFound();
            }
            return View(roleUser);
        }

        // POST: RoleUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Role")] RoleUser roleUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roleUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roleUser);
        }

        // GET: RoleUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleUser roleUser = db.RoleUsers.Find(id);
            if (roleUser == null)
            {
                return HttpNotFound();
            }
            return View(roleUser);
        }

        // POST: RoleUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoleUser roleUser = db.RoleUsers.Find(id);
            db.RoleUsers.Remove(roleUser);
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
