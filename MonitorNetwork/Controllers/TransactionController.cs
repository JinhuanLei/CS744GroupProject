using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MonitorNetwork.Database;

namespace MonitorNetwork.Views
{
    public class TransactionController : Controller
    {
        private MNDatabase db = new MNDatabase();

        // GET: Transaction
        public ActionResult Index()
        {
            var transaction = db.transaction.Include(t => t.account).Include(t => t.store);
            return View(transaction.ToList());
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = db.transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transaction/Create
        public ActionResult Create()
        {
            ViewBag.accountID = new SelectList(db.account, "accountID", "accountFirstName");
            ViewBag.storeID = new SelectList(db.store, "storeID", "storeIP");
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "transactionID,timeOfTransaction,timeOfResponse,amount,isCredit,status,isEncrypted,isSent,storeID,accountID")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.transaction.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.accountID = new SelectList(db.account, "accountID", "accountFirstName", transaction.accountID);
            ViewBag.storeID = new SelectList(db.store, "storeID", "storeIP", transaction.storeID);
            return View(transaction);
        }

        // GET: Transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = db.transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountID = new SelectList(db.account, "accountID", "accountFirstName", transaction.accountID);
            ViewBag.storeID = new SelectList(db.store, "storeID", "storeIP", transaction.storeID);
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "transactionID,timeOfTransaction,timeOfResponse,amount,isCredit,status,isEncrypted,isSent,storeID,accountID")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accountID = new SelectList(db.account, "accountID", "accountFirstName", transaction.accountID);
            ViewBag.storeID = new SelectList(db.store, "storeID", "storeIP", transaction.storeID);
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = db.transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            transaction transaction = db.transaction.Find(id);
            db.transaction.Remove(transaction);
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
