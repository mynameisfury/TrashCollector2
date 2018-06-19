using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector2.Models;

namespace TrashCollector2.Controllers
{
    public class PickupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult CustomerPickups()
        {
            string userID = User.Identity.GetUserId();
            var customer = db.Customers.Where(c => c.UserID == userID).FirstOrDefault();
            var pickups = db.Pickups.Where(p => p.CustomerID == customer.ID && p.Complete == false);
            return View(pickups.ToList());
        }

        public ActionResult CompletePickup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            pickup.Complete = true;
            pickup.Customer.Balance += pickup.Charge;
            var newpickup = new Pickup();
            newpickup.CustomerID = pickup.CustomerID;
            var OneWeek = new System.TimeSpan(7, 0, 0, 0);
            newpickup.PickupDate = pickup.PickupDate.Add(OneWeek);
            if (pickup.Suspended != false && pickup.OneTime == false)
            {
                db.Pickups.Add(newpickup);
            }
            db.SaveChanges();
            return View(pickup);
        }

        public ActionResult SuspendPickup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            
            pickup.PickupDate = pickup.SuspensionDate ?? pickup.PickupDate;
            pickup.SuspensionDate = null;
            return View(pickup);
        }

        // GET: Pickups
        public ActionResult Index()
        {
            var pickups = db.Pickups.Include(p => p.Customer).Include(p => p.Worker);
            return View(pickups.ToList());
        }

        // GET: Pickups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        // GET: Pickups/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FirstName");
            ViewBag.WorkerID = new SelectList(db.Workers, "ID", "Username");
            return View();
        }

        // POST: Pickups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PickupDate,CustomerID,WorkerID,Charge,Complete,Suspended,SuspensionDate,OneTime")] Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                string userID = User.Identity.GetUserId();
                var customer = db.Customers.Where(c => c.UserID == userID).FirstOrDefault();
                pickup.CustomerID = customer.ID;
                pickup.Charge = 5;
                db.Pickups.Add(pickup);
                db.SaveChanges();
                return RedirectToAction("CustomerPickups");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FirstName", pickup.CustomerID);
            ViewBag.WorkerID = new SelectList(db.Workers, "ID", "Username", pickup.WorkerID);
            return View(pickup);
        }

        // GET: Pickups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FirstName", pickup.CustomerID);
            ViewBag.WorkerID = new SelectList(db.Workers, "ID", "Username", pickup.WorkerID);
            return View(pickup);
        }

        // POST: Pickups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PickupDate,CustomerID,WorkerID,Charge,Complete,Suspended,SuspensionDate,OneTime")] Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FirstName", pickup.CustomerID);
            ViewBag.WorkerID = new SelectList(db.Workers, "ID", "Username", pickup.WorkerID);
            return View(pickup);
        }

        // GET: Pickups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        // POST: Pickups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pickup pickup = db.Pickups.Find(id);
            db.Pickups.Remove(pickup);
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
