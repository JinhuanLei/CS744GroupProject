﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MonitorNetwork.Database;

namespace MonitorNetwork.Controllers
{
    public class TransactionController : Controller
    {
        private MNDatabase db = new MNDatabase();

        // GET: Transaction
        //public ActionResult Index()
        //{
        //    var transaction = db.transaction.Include(t => t.account).Include(t => t.store);
        //    return View(transaction.ToList());
        //}

        // GET: Transaction/Create
        public ActionResult Create()
        {
            var accountInfo = from acct in db.account
                              select new { accountID = acct.accountID, fullname = acct.accountFirstName + " " + acct.accountLastName };

            ViewBag.accountID = new SelectList(accountInfo, "accountID", "fullname");
            ViewBag.storeID = new SelectList(db.store, "storeID", "merchantName");
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "transactionID,timeOfTransaction,timeOfResponse,amount,isCredit,status,isEncrypted,isSent,storeID,accountID,isSelf")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.transaction.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            var accountInfo = from acct in db.account
                              select new { accountID = acct.accountID, fullname = acct.accountFirstName + " " + acct.accountLastName };

            ViewBag.accountID = new SelectList(accountInfo, "accountID", "fullname", transaction.accountID);
            ViewBag.storeID = new SelectList(db.store, "storeID", "merchantName", transaction.storeID);
            return View(transaction);
        }

        //public ActionResult Encrypt(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    transaction transaction = db.transaction.Find(id);
        //    if (transaction == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    transaction.isEncrypted = true;

        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        #region Unused features

        //// GET: Transaction/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    transaction transaction = db.transaction.Find(id);
        //    if (transaction == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(transaction);
        //}

        //// GET: Transaction/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    transaction transaction = db.transaction.Find(id);
        //    if (transaction == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.accountID = new SelectList(db.account, "accountID", "accountFirstName", transaction.accountID);
        //    ViewBag.storeID = new SelectList(db.store, "storeID", "storeIP", transaction.storeID);
        //    return View(transaction);
        //}

        //// POST: Transaction/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //public ActionResult Edit([Bind(Include = "transactionID,timeOfTransaction,timeOfResponse,amount,isCredit,status,isEncrypted,isSent,storeID,accountID")] transaction transaction)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(transaction).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.accountID = new SelectList(db.account, "accountID", "accountFirstName", transaction.accountID);
        //    ViewBag.storeID = new SelectList(db.store, "storeID", "storeIP", transaction.storeID);
        //    return View(transaction);
        //}

        //// GET: Transaction/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    transaction transaction = db.transaction.Find(id);
        //    if (transaction == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(transaction);
        //}

        //// POST: Transaction/Delete/5
        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    transaction transaction = db.transaction.Find(id);
        //    db.transaction.Remove(transaction);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        #endregion

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