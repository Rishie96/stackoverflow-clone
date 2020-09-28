using StackOverflow.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverflow.Controllers
{
    public class UserController : Controller
    {
        [Authorize]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [Route("userinfo")]
        [HttpGet]
        public ActionResult UserInfo()
        {
            string UserID = Session["UserID"].ToString();
            ViewBag.UserID = UserID;
            return View();
        }
        [Route("userinfo")]
        [HttpPost]
        public ActionResult UserInfo(UserDetails model)
        {
            UserContext context = new UserContext();
            context.Details.Add(model);
            context.SaveChanges();
            return Redirect("/home");
        }
        [Route("editprofile")]
        [HttpGet]
        public ActionResult EditProfile(int? updated = 0)
        {
            if (Session["UserID"] != null)
            {
                UserContext context = new UserContext();
                string UserID = Session["UserID"].ToString();
                UserDetails details = context.Details.FirstOrDefault(u => u.UserID == UserID);
                ViewBag.Updated = updated;
                ViewBag.UserDetails = details;
                return View();
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Route("editprofile")]
        [HttpPost]
        public ActionResult EditProfile(UserDetails user)
        {
            UserContext context = new UserContext();
            context.Details.AddOrUpdate(user);
            context.SaveChanges();
            Session["FirstName"] = user.FirstName;
            return RedirectToAction("EditProfile", "User", new { updated = 1 });
        }
    }
}