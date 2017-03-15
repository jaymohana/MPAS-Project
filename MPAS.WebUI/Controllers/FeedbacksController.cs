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
    public class FeedbacksController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: Feedbacks
        public ActionResult Index()
        {


            return View(db.Feedbacks.ToList());
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {

            var mentorTemp = db.Users.ToList();
            var mentorList = new List<string>();
            var menteeList = new List<string>();

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
                        else if (item.UserName == "" || (!item.UserName.Contains("@")))
                        {

                        }
                        else
                        {
                            item.UserName = item.UserName.Substring(0, item.UserName.IndexOf("@"));
                        }
                        mentorList.Add(item.UserName);
                    }
                    else if (item.Roles.ElementAt(p).RoleId == "1")
                    {
                        if (item.UserName.Contains("@") && item.UserName.Substring(0, item.UserName.IndexOf("@")).Contains("."))
                        {
                            item.UserName = item.UserName.Split('.')[0] + " " + item.UserName.Split('.')[1].Substring(0, item.UserName.Split('.')[1].IndexOf("@"));
                        }
                        else if (item.UserName == "" || !item.UserName.Contains("@"))
                        {

                        }
                        else
                        {
                            item.UserName = item.UserName.Substring(0, item.UserName.IndexOf("@"));
                        }
                        menteeList.Add(item.UserName);
                    }
                }

            }

            SelectList list = new SelectList(mentorList, "Id");
            ViewBag.chats = list;

            SelectList list2 = new SelectList(menteeList, "Id");
            ViewBag.menteeList = list2;

            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MentorName,Date,SessionNumber,MeetingLength,Venue,MenteeName,MeetingSummary,WorriedStatus,AdditionalInfo,User_Id")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MentorName,Date,SessionNumber,MeetingLength,Venue,MenteeName,MeetingSummary,WorriedStatus,AdditionalInfo,User_Id")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
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

        public ActionResult getMentors(List<Professor> userList)
        {
            userList = db.Professors.ToList(); 
            
            return View();
        }

    }
}
