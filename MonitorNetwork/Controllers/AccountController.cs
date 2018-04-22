using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MonitorNetwork.BLL;
using MonitorNetwork.Database;
using MonitorNetwork.Models;

namespace MonitorNetwork.Controllers
{
    [CheckAuthorization]
    public class AccountController : Controller
    {
        private MNDatabase db = new MNDatabase();

        // GET: accounts
        public ActionResult Index()
        {
            return View(db.account.ToList());
        }

        // GET: accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(CreditCardAndAccountViewModel creditCardAndAccount)
        {
            if (db.creditcard.Where(x => x.cardNumber == creditCardAndAccount.creditcard.cardNumber).Count() > 0)
            {
                ModelState.AddModelError("creditcard.cardNumber", "Credit card number all ready exists.");
            }

            if (ModelState.IsValid)
            {
                creditCardAndAccount.account.creditcard.Add(creditCardAndAccount.creditcard);
                db.account.Add(creditCardAndAccount.account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(creditCardAndAccount);
        }

        // GET: accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "accountID,accountFirstName,accountLastName,address,phoneNumber,spendingLimit,balance")] account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        [HttpPost]
        public ActionResult DeleteCheckAccount(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            account account = db.account.Find(id);

            if (account == null)
            {
                return HttpNotFound();
            }

            if (account.balance > 0)
            {
                return Json("NON_ZERO_BALANCE");
            }

            return Json("DELETE");
        }

        // POST: accounts/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            account account = db.account.Find(id);
            db.account.Remove(account);
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
