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
    public class ChatroomsController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: Chatrooms
        public ActionResult Index()
        {
            List<Chatroom> chatroomList = db.Chatroom.ToList();
            List<string> messagesList = new List<string>();

            foreach (var item in chatroomList)
            {
                messagesList.Add(item.Message);
            }
            chatroomList.Reverse();

            ViewBag.chats = chatroomList;

            return View();
        }

        // GET: Chatrooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chatroom chatroom = db.Chatroom.Find(id);
            if (chatroom == null)
            {
                return HttpNotFound();
            }
            return View(chatroom);
        }

        // GET: Chatrooms/Create
        public ActionResult Create()
        {
            //List<Chatroom> chatroomList = db.Chatroom.ToList();
            //List<string> messagesList = new List<string>();

            //foreach (var item in chatroomList)
            //{
            //    messagesList.Add(item.Message);
            //}

            List<Chatroom> chatroomList = db.Chatroom.ToList();
            List<string> messagesList = new List<string>();

            foreach (var item in chatroomList)
            {
                messagesList.Add(item.Message);

                if ((item.posterUserName != "") && item.posterUserName.Substring(0, item.posterUserName.IndexOf("@")).Contains("."))
                {
                    item.posterUserName = item.posterUserName.Split('.')[0] + " " + item.posterUserName.Split('.')[1].Substring(0, item.posterUserName.Split('.')[1].IndexOf("@"));
                }
                else if (item.posterUserName == "")
                {

                }
                else
                {
                    item.posterUserName = item.posterUserName.Substring(0, item.posterUserName.IndexOf("@"));
                }




            }
            chatroomList.Reverse();

            ViewBag.chats = chatroomList;

            return View();
        }

        // POST: Chatrooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Message")] Chatroom chatroom)
        {
            if (ModelState.IsValid)
            {
                chatroom.posterUserName = User.Identity.Name;
                db.Chatroom.Add(chatroom);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(chatroom);
        }

        // GET: Chatrooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chatroom chatroom = db.Chatroom.Find(id);
            if (chatroom == null)
            {
                return HttpNotFound();
            }
            return View(chatroom);
        }

        // POST: Chatrooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Message")] Chatroom chatroom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chatroom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chatroom);
        }

        // GET: Chatrooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chatroom chatroom = db.Chatroom.Find(id);
            if (chatroom == null)
            {
                return HttpNotFound();
            }
            return View(chatroom);
        }

        // POST: Chatrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chatroom chatroom = db.Chatroom.Find(id);
            db.Chatroom.Remove(chatroom);
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
