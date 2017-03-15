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
    public class MenteeFeedbacksController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: MenteeFeedbacks
        public ActionResult Index()
        {
            return View(db.MenteeFeedbacks.ToList());
        }

        // GET: MenteeFeedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenteeFeedback menteeFeedback = db.MenteeFeedbacks.Find(id);
            if (menteeFeedback == null)
            {
                return HttpNotFound();
            }

            return View(menteeFeedback);
        }


        // GET: MenteeFeedbacks/Create
        public ActionResult Create()
        {

            var mentorTemp = db.Users.ToList();
            var mentorList = new List<string>();

            foreach (var item in mentorTemp)
            {
                for (int p = 0; p < item.Roles.Count; p++)
                {
                    if (item.Roles.ElementAt(p).RoleId == "2")
                    {
                        if ((item.UserName != "") && (item.UserName.Contains("@")) && item.UserName.Substring(0, item.UserName.IndexOf("@")).Contains("."))
                        {
                            item.UserName = item.UserName.Split('.')[0] + " " + item.UserName.Split('.')[1].Substring(0, item.UserName.Split('.')[1].IndexOf("@"));
                        }
                        else if (item.UserName == "" && (!item.UserName.Contains("@")))
                        {

                        }
                        else
                        {
                            item.UserName = item.UserName.Substring(0, item.UserName.IndexOf("@"));
                        }
                        mentorList.Add(item.UserName);
                    }

                }

            }

            SelectList list = new SelectList(mentorList, "Id");
            ViewBag.fruitlist = list;

                return View();
        }

        // POST: MenteeFeedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, MentorName,Attendance,Rating,FeedbackMessage,User_Id")] MenteeFeedback menteeFeedback)
        {
            if (ModelState.IsValid)
            {
                db.MenteeFeedbacks.Add(menteeFeedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menteeFeedback);
        }

        // GET: MenteeFeedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenteeFeedback menteeFeedback = db.MenteeFeedbacks.Find(id);
            if (menteeFeedback == null)
            {
                return HttpNotFound();
            }
            return View(menteeFeedback);
        }

        // POST: MenteeFeedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MenteeName,Rating,Attendance,FeedbackMessage")] MenteeFeedback menteeFeedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menteeFeedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menteeFeedback);
        }

        // GET: MenteeFeedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenteeFeedback menteeFeedback = db.MenteeFeedbacks.Find(id);
            if (menteeFeedback == null)
            {
                return HttpNotFound();
            }
            return View(menteeFeedback);
        }

        // POST: MenteeFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenteeFeedback menteeFeedback = db.MenteeFeedbacks.Find(id);
            db.MenteeFeedbacks.Remove(menteeFeedback);
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
