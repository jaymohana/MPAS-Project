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
    public class MeetingsController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: Meetings
        public ActionResult Index()
        {
            if (User.Identity.Name != "")
            {
                List<Meeting> meet = db.Meetings.ToList();
                bool foundMeeting = false;
                int index = -1;
                foreach (var item in meet)
                {
                    if (item.professorID == User.Identity.Name)
                    {
                        foundMeeting = true;
                    }
                }
                
                if(!foundMeeting)
                {
                    return View(db.Meetings.ToList());
                }
                else
                {
                    return RedirectToAction("Join");
                }
            }
            else
            {
                //if user isn't signed in the program redirects to bad request should rather be changed to restricted
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
        }

        // GET: Meetings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // GET: Meetings/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Join()
        {
             return View(db.Meetings.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join(int? id)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Join");
            }

            return View();
        }


        // POST: Meetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,mentorName,venueName,postDate,available")] Meeting meeting)
        {
             if (ModelState.IsValid)
            {
                db.Meetings.Add(meeting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meeting);
        }

        // GET: Meetings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (User.Identity.Name != "")
            {
                Meeting meeting = db.Meetings.Find(id);
                List<Meeting> meet = db.Meetings.ToList();
                Meeting foundMeeting;
                int index = -1;
                int indexRemove = -1;
                int count = 0;

                foreach(var item in meet)
                {
                    indexRemove++;
                    if(item.professorID == User.Identity.Name)
                    {
                        meet[indexRemove].available = meet[indexRemove].available + 1;
                        meet[indexRemove].professorID = null;
                    }
                }

                foreach (var item in meet)
                {
                    index++;
                    //Can't set all to null. Remove line below.
                    //meet[index].professorID = null;

                    if (item.Id == id)
                    {
                        count = index;
                    }
                }
                meet[count].professorID = User.Identity.Name;
                meet[count].available = meet[count].available - 1;
                db.Entry(meeting).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                //if user isn't signed in the program redirects to bad request should rather be changed to restricted
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            //bool isinrole = User.IsInRole("Mentee");
            //var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new EFDbContext()));
            
            //if (!roleManager.RoleExists("ROLE NAME"))
            //{
            //    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //    role.Name = "ROLE NAME";
            //    roleManager.Create(role);
            //}


            return RedirectToAction("Join");
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,mentorName,postDate")] Meeting meeting)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(meeting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            db.Meetings.Remove(meeting);
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
