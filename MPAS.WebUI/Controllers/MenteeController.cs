using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MPAS.WebUI.Models;
using MPAS.Domain.Entities;
using MPAS.Domain.Context;
using System.Net;
using System.Data.Entity;

namespace MPAS.WebUI.Controllers
{
    public class MenteeController : Controller
    {

        private EFDbContext db = new EFDbContext();
        public ActionResult Index()
        {
            var mentees = db.Mentees.Include(p => p.User);
            return View(mentees.ToList());
        }

        // GET: Professors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mentee mentee = db.Mentees.Find(id);
            if (mentee == null)
            {
                return HttpNotFound();
            }
            return View(mentee);
        }

        // GET: Professors/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId")] Mentee mentee)
        {
            if (ModelState.IsValid)
            {
                db.Mentees.Add(mentee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", mentee.UserId);
            return View(mentee);
        }

        // GET: Professors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mentee mentee = db.Mentees.Find(id);
            if (mentee == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", mentee.UserId);
            return View(mentee);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId")] Mentee mentee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mentee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", mentee.UserId);
            return View(mentee);
        }

        // GET: Professors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mentee mentee = db.Mentees.Find(id);
            if (mentee == null)
            {
                return HttpNotFound();
            }
            return View(mentee);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mentee mentee = db.Mentees.Find(id);
            db.Mentees.Remove(mentee);
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