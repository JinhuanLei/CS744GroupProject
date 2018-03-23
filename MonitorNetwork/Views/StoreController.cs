using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MonitorNetwork.Database;
using MonitorNetwork.Models;

namespace MonitorNetwork.Views
{
    public class StoreController : Controller
    {
        private MNDatabase db = new MNDatabase();

        // GET: Store
        public ActionResult Index()
        {
            var store = db.store.Include(s => s.region);
            return View(store.ToList());
        }

        // GET: Store/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            store store = db.store.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            ViewBag.regionID = new SelectList(db.region, "regionID", "regionID");
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "storeID,storeIP,merchantName,regionID")] store store)
        {
            ViewBag.regionID = new SelectList(db.region, "regionID", "regionID", store.regionID);
            TempData["regionID"] = store.regionID.ToString();
            return RedirectToAction("Create2");
        
        }
        public ActionResult Create2(store store)
        {
            int regionID = Int32.Parse(TempData["regionID"].ToString());
            var relays = db.relay.Where(x => x.regionID == regionID);
            //ViewBag.relayInfo = relays;
            StoreModel sm = new StoreModel();
            sm.relays = relays.ToArray();
            if (ModelState.IsValid)
            {
                db.store.Add(store);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sm);
        }
        // GET: Store/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            store store = db.store.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            ViewBag.regionID = new SelectList(db.region, "regionID", "regionID", store.regionID);
            return View(store);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "storeID,storeIP,merchantName,regionID")] store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.regionID = new SelectList(db.region, "regionID", "regionID", store.regionID);
            return View(store);
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            store store = db.store.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            store store = db.store.Find(id);
            db.store.Remove(store);
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
