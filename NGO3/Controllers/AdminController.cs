using NGO3.Data;
using NGO3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGO3.Controllers
{
    public class AdminController : Controller
    {
        private MyDbContext db=new MyDbContext();   
        // GET: Admin
        public ActionResult Index()
        {
            if(Session["AdminId"] == null)
            {
                return RedirectToAction("AdminLogin");
            }
            return View();
        }
        
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var a = db.users.Where(model => model.Email == user.Email && model.Password == user.Password && model.IsAdmin ==true).FirstOrDefault();
                if (a == null)
                {
                    ViewBag.loginmassage = "Login Failed";
                    return View();
                }
                else
                {
                    Session["AdminId"] = a.Uid;
                    
                    return RedirectToAction("Index");
                }
            }

            return View(user);
        }
    }
}