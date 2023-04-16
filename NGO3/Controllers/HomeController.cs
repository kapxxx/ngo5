using NGO3.Data;
using NGO3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;

namespace NGO3.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext db= new MyDbContext();
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "ContactusId,Name,Email,Mobile,Massage")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                db.contactUs.Add(contactUs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactUs);
        }

        public ActionResult Blogs()
    {
          var blog = db.blogs.ToList();
          return View(blog);
    }



    //-------Donation Lists------
    public ActionResult Donation()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login");
            }
            var donations = db.donations.Where(x=>x.IsApproved).Include(d => d.Category).ToList();
            ViewBag.donationlist = donations;
            return View(donations);

        }


        //============================User Authentication=============================
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);    
        }

        //-------User Register------   
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        //-------User Login------
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var a = db.users.Where(model => model.Email == user.Email && model.Password == user.Password).FirstOrDefault();
                if (a == null)
                {
                    ViewBag.loginmassage = "Login Failed";
                    return View();
                }
                else
                {
                    Session["UserId"] = a.Uid;
                    Session["Username"] = a.Name;
                    return RedirectToAction("Index");
                }
            }

            return View(user);
        }

        //-------Logout--------
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        //================X============User Authentication===============X==============

        //-------Fund Add from user side--------
        [HttpGet]
        public ActionResult AddFund(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Home");
            }
            ViewBag.Did = id;
            ViewBag.Uid = Session["UserId"];
            return View();
        }
        
        [HttpPost]
        public ActionResult AddFund(Fund fund)
        {
            if (ModelState.IsValid)
            {
                db.funds.Add(fund);
                db.SaveChanges();

                var donationtable = db.donations.Find(fund.Did);
                if (donationtable != null)
                {
                    donationtable.RaisedAmount += fund.DonationMoney;
                    db.Entry(donationtable).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

            ViewBag.Did = fund.Did;
            ViewBag.Uid = Session["UserId"];
            return View(fund);         
        }

        //-----------------My Donation Funds--------------
        [HttpGet]
        public ActionResult MydonatonFund()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login");
            }
            int uid = Convert.ToInt32(Session["UserId"]);
            var myfunds = db.funds.Where(x => x.Uid == uid).ToList();
            return View(myfunds);
        }



        //-----------------Raise Fund Funds--------------
        [HttpGet]
        public ActionResult RaiseFund()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Cid = new SelectList(db.categories, "Cid", "Name");
            ViewBag.Uid = Session["UserId"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RaiseFund(Donation donation, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Server.MapPath("~/Content/DonationImage/" + fileName);
                file.SaveAs(path);
                if (System.IO.File.Exists(path))
                {
                    donation.Image = fileName;
                    donation.RaisedAmount = 0;

                    db.donations.Add(donation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("Image", "Please upload an image");
            }

            ViewBag.Cid = new SelectList(db.categories, "Cid", "Name", donation.Cid);
            return View(donation);
        }



    }
}