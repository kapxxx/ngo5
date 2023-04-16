using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NGO3.Data;
using NGO3.Models;

namespace NGO3.Controllers
{
    public class DonationsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Donations
        public ActionResult Index()
        {
            var donations = db.donations.Include(d => d.Category);
            return View(donations.ToList());
        }

        // GET: Donations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // GET: Donations/Create
        public ActionResult Create()
        {
            ViewBag.Cid = new SelectList(db.categories, "Cid", "Name");
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Donation donation, HttpPostedFileBase file)
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

        // GET: Donations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cid = new SelectList(db.categories, "Cid", "Name", donation.Cid);
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DId,Name,Image,Description,TargetAmount,RaisedAmount,TargetReachDate,Reason,IsApproved,AdharCardNumber,createdby,Cid")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cid = new SelectList(db.categories, "Cid", "Name", donation.Cid);
            return View(donation);
        }

        // GET: Donations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donation donation = db.donations.Find(id);
            db.donations.Remove(donation);
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

        
        public ActionResult ApproveDonation(int id)
        {
            var donation = db.donations.Find(id);
            donation.IsApproved = true;
            db.Entry(donation).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        
    }
}
