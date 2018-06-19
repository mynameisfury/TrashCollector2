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
    public class AddressesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult CustomerAddresses()
        {
            string userID = User.Identity.GetUserId();
            var customer = db.Customers.Where(c => c.UserID == userID).FirstOrDefault();
            var addresses = db.Addresses.Where(a => a.UserId == customer.UserID);
            if (customer.AddressID == null)
            {
                return RedirectToAction("Create");
            }
            else return View(addresses.ToList());
    }
        // GET: Addresses
        public ActionResult Index()
        {
            var addresses = db.Addresses.Include(a => a.City).Include(a => a.State);
            return View(addresses.ToList());
        }

        // GET: Addresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityName");
            ViewBag.StateID = new SelectList(db.States, "ID", "StateName");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StreetAddressLine1,StrettAddressLine2,CityID,StateID,ZipCode,UserId,Latitude,Longitude")] Address address)
        {
            if (ModelState.IsValid)
            {
                string userID = User.Identity.GetUserId();
                var customer = db.Customers.Where(c => c.UserID == userID).FirstOrDefault();
                var worker = db.Workers.Where(w => w.UserID == userID).FirstOrDefault();
                if (customer != null)
                {
                    customer.AddressID = address.ID;
                }
                else if (worker != null)
                {
                    worker.AddressID = address.ID;
                }
                address.UserId = User.Identity.GetUserId();
                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityName", address.CityID);
            ViewBag.StateID = new SelectList(db.States, "ID", "StateName", address.StateID);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityName", address.CityID);
            ViewBag.StateID = new SelectList(db.States, "ID", "StateName", address.StateID);
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StreetAddressLine1,StrettAddressLine2,CityID,StateID,ZipCode,UserId,Latitude,Longitude")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityName", address.CityID);
            ViewBag.StateID = new SelectList(db.States, "ID", "StateName", address.StateID);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
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
