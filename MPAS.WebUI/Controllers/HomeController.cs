using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MPAS.Domain.Context;
using MPAS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MPAS.WebUI.Controllers
{
    public class HomeController : Controller
    {
        EFDbContext db = new EFDbContext();
        public ActionResult Index()
        {
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(db));

            if ((User.Identity.Name != "")&&(!userManager.GetRoles(User.Identity.GetUserId()).Contains("Mentee")))
            {
                userManager.AddToRole(User.Identity.GetUserId(), "Mentee");
            }

                return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(FormCollection collection)
        {
            var query = collection.Get("q");
            return RedirectToAction("Search", "Home", new { query = query });
        }
        [HttpGet]
        public ActionResult Search(string query)
        {
            if (query == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                string[] words = query.Split(' ');
                List<string> keywords = words.ToList();
                List<User> users = db.Users.ToList();
                List<User> resultUsers = new List<Domain.Entities.User>();
                foreach (var key in keywords)
                {
                    var user = (from x in db.Users where x.Email.Contains(key) select x).FirstOrDefault();
                    if (user != null)
                    {
                        resultUsers.Add(user);
                    }
                }

                ViewBag.ResultUsers = resultUsers;

                return View();
            }
           
        }
    }
}