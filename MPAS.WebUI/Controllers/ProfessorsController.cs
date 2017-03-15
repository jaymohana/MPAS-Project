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

namespace MPAS.WebUI.Controllers
{
    public class ProfessorsController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: Professors
        public ActionResult Index()
        {
            // var professors = db.Professors.Include(p => p.User);
            //return View(professors.ToList());
            return View(db.Professors.ToList());
        }

        // GET: Professors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // GET: Professors/Create
        public ActionResult Create()
        {
          //  ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Description,ImagePath,CategoryID,Email")] Professor professor, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var aPath = "/Icons/" + Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName);//added
                    upload.SaveAs(Server.MapPath("~" + aPath));
                    professor.ImagePath = aPath;
                }

                db.Professors.Add(professor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //  ViewBag.UserId = new SelectList(db.Users, "Id", "Email", professor.CategoryID);
            return View(professor);
        }


        // GET: Professors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
          //  ViewBag.UserId = new SelectList(db.Users, "Id", "Email", professor.CategoryID);
            return View(professor);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Description,ImagePath,CategoryID,Email")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Email", professor.CategoryID);
            return View(professor);
        }

        // GET: Professors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Professor professor = db.Professors.Find(id);
            db.Professors.Remove(professor);
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
