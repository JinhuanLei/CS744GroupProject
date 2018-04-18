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

namespace MonitorNetwork.Controllers
{
    [CheckAuthorization]
    public class TransactionController : Controller
    {
        private MNDatabase db = new MNDatabase();

        // GET: Transaction/Create
        public ActionResult Create()
        {
            var creditcardInfo = from creditcard in db.creditcard
                                 select new { creditcard.cardID, fullname = creditcard.customerFirstName + " " + creditcard.customerLastName };
            ViewBag.cardID = new SelectList(creditcardInfo, "cardID", "fullname");
            var storeInfo = from store in db.store
                            select new { store.storeID, storeName = store.merchantName + " (" + store.storeIP.Substring(8) + ")" };
            ViewBag.storeID = new SelectList(storeInfo, "storeID", "storeName");
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(transaction transaction)
        {
            if (ModelState.IsValid)
            {
				transaction.isProcessed = false;
                db.transaction.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            var creditcardInfo = from creditcard in db.creditcard
                                 select new { creditcard.cardID, fullname = creditcard.customerFirstName + " " + creditcard.customerLastName };

            ViewBag.cardID = new SelectList(creditcardInfo, "cardID", "fullname");
            var storeInfo = from store in db.store
                            select new { store.storeID, storeName = store.merchantName + " (" + store.storeIP.Substring(8) + ")" };
            ViewBag.storeID = new SelectList(storeInfo, "storeID", "storeName");

            return View(transaction);
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
