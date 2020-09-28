using Microsoft.AspNet.Identity;
using StackOverflow.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("index")]
        [Route("home")]
        [Route("home/index")]
        public ActionResult Index()
        {
            ApplicationUser currentUser = new ApplicationUser();
            // Check if user is logged in
            if (Session["UserID"] != null)
            {
                // Following service is used to check if user has filled details
                UserServices services = new UserServices();
                UserDetails userDetails = services.GetUserDetails(Session["UserID"].ToString());
                // If user has not filled details, request user to do so. Else, simply return to home page
                if (userDetails != null)
                {
                    Session["FirstName"] = userDetails.FirstName;
                    return View();
                } else
                {
                    return Redirect("/userinfo");
                }
            } else
            {
                ApplicationDbContext db = new ApplicationDbContext();
                string currentUserID = User.Identity.GetUserId();
                currentUser = db.Users.FirstOrDefault(u => u.Id == currentUserID);
                return View(currentUser);
            }
        }
        [Route("about")]
        public ActionResult About()
        {
            return View();
        }
        [Route("contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}