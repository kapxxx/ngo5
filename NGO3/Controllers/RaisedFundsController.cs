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
    public class RaisedFundsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: RaisedFunds
        public ActionResult Index()
        {
            var raisedFunds = db.raisedFunds.Include(r => r.Category).Include(r => r.User);
            return View(raisedFunds.ToList());
        }

        // GET: RaisedFunds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaisedFund raisedFund = db.raisedFunds.Find(id);
            if (raisedFund == null)
            {
                return HttpNotFound();
            }
            return View(raisedFund);
        }

        // GET: RaisedFunds/Create
        public ActionResult Create()
        {
            ViewBag.Cid = new SelectList(db.categories, "Cid", "Name");
            ViewBag.Uid = new SelectList(db.users, "Uid", "Name");
            return View();
        }

        // POST: RaisedFunds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RaisId,Reason,AdharCardNumber,Amount,IsApporuved,Cid,Uid")] RaisedFund raisedFund)
        {
            if (ModelState.IsValid)
            {
                db.raisedFunds.Add(raisedFund);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cid = new SelectList(db.categories, "Cid", "Name", raisedFund.Cid);
            ViewBag.Uid = new SelectList(db.users, "Uid", "Name", raisedFund.Uid);
            return View(raisedFund);
        }

        // GET: RaisedFunds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaisedFund raisedFund = db.raisedFunds.Find(id);
            if (raisedFund == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cid = new SelectList(db.categories, "Cid", "Name", raisedFund.Cid);
            ViewBag.Uid = new SelectList(db.users, "Uid", "Name", raisedFund.Uid);
            return View(raisedFund);
        }

        // POST: RaisedFunds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RaisId,Reason,AdharCardNumber,Amount,IsApporuved,Cid,Uid")] RaisedFund raisedFund)
        {
            if (ModelState.IsValid)
            {
                db.Entry(raisedFund).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cid = new SelectList(db.categories, "Cid", "Name", raisedFund.Cid);
            ViewBag.Uid = new SelectList(db.users, "Uid", "Name", raisedFund.Uid);
            return View(raisedFund);
        }

        // GET: RaisedFunds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaisedFund raisedFund = db.raisedFunds.Find(id);
            if (raisedFund == null)
            {
                return HttpNotFound();
            }
            return View(raisedFund);
        }

        // POST: RaisedFunds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RaisedFund raisedFund = db.raisedFunds.Find(id);
            db.raisedFunds.Remove(raisedFund);
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
