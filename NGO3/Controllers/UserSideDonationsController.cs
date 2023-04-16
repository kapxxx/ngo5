using NGO3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace NGO3.Controllers
{
    public class UserSideDonationsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Donations
        public ActionResult Index()
        {
            var donations = db.donations.Include(d => d.Category).ToList();
            return View(donations);
        }
    }
}