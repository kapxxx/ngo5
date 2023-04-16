using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NGO3.Data;
using NGO3.Models;

namespace NGO3.Controllers
{
    public class FundsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Funds
        public ActionResult Index()
        {
            var funds = db.funds.Include(f => f.Donation).Include(f => f.UserData);
            return View(funds.ToList());
        }

        // GET: Funds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fund fund = db.funds.Find(id);
            if (fund == null)
            {
                return HttpNotFound();
            }
            return View(fund);
        }

        // GET: Funds/Create
        public ActionResult Create()
        {
            ViewBag.Did = new SelectList(db.donations, "DId", "Name");
            ViewBag.Uid = new SelectList(db.users, "Uid", "Name");
            return View();
        }

        // POST: Funds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Fid,DonationMoney,Uid,Did")] Fund fund)
        {
            if (ModelState.IsValid)
            {
                db.funds.Add(fund);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Did = new SelectList(db.donations, "DId", "Name", fund.Did);
            ViewBag.Uid = new SelectList(db.users, "Uid", "Name", fund.Uid);
            return View(fund);
        }

        // GET: Funds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fund fund = db.funds.Find(id);
            if (fund == null)
            {
                return HttpNotFound();
            }
            ViewBag.Did = new SelectList(db.donations, "DId", "Name", fund.Did);
            ViewBag.Uid = new SelectList(db.users, "Uid", "Name", fund.Uid);
            return View(fund);
        }

        // POST: Funds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Fid,DonationMoney,Uid,Did")] Fund fund)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fund).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Did = new SelectList(db.donations, "DId", "Name", fund.Did);
            ViewBag.Uid = new SelectList(db.users, "Uid", "Name", fund.Uid);
            return View(fund);
        }

        // GET: Funds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fund fund = db.funds.Find(id);
            if (fund == null)
            {
                return HttpNotFound();
            }
            return View(fund);
        }

        // POST: Funds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fund fund = db.funds.Find(id);
            db.funds.Remove(fund);
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
