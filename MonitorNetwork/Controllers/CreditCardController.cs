using System;
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
    public class CreditCardController : Controller
    {
        private MNDatabase db = new MNDatabase();

        // GET: CreditCard
        public ActionResult Index()
        {
            var creditcard = db.creditcard.Include(c => c.account);
            return View(creditcard.ToList());
        }

        // GET: CreditCard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            creditcard creditcard = db.creditcard.Find(id);
            if (creditcard == null)
            {
                return HttpNotFound();
            }
            return View(creditcard);
        }

        // GET: CreditCard/Create
        public ActionResult Create()
        {
            ViewBag.accountID = new SelectList((from acct in db.account select new { accountID = acct.accountID, fullname = acct.accountFirstName + " " + acct.accountLastName }), "accountID", "fullname");
            return View();
        }

        // POST: CreditCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "cardID,cardNumber,expirationDate,securityCode,customerFirstName,customerLastName,accountID")] creditcard creditcard)
        {
            if (ModelState.IsValid)
            {
                db.creditcard.Add(creditcard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.accountID = new SelectList((from acct in db.account select new { accountID = acct.accountID, fullname = acct.accountFirstName + " " + acct.accountLastName }), "accountID", "fullname", creditcard.accountID);
            return View(creditcard);
        }

        // GET: CreditCard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            creditcard creditcard = db.creditcard.Find(id);
            if (creditcard == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountID = new SelectList((from acct in db.account select new {accountID=acct.accountID, fullname = acct.accountFirstName + " " + acct.accountLastName }), "accountID", "fullname", creditcard.accountID);
            return View(creditcard);
        }

        // POST: CreditCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "cardID,cardNumber,expirationDate,securityCode,customerFirstName,customerLastName,accountID")] creditcard creditcard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creditcard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accountID = new SelectList((from acct in db.account select new { accountID = acct.accountID, fullname = acct.accountFirstName + " " + acct.accountLastName }), "accountID", "fullname", creditcard.accountID);
            return View(creditcard);
        }

        // GET: CreditCard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            creditcard creditcard = db.creditcard.Find(id);
            if (creditcard == null)
            {
                return HttpNotFound();
            }
            return View(creditcard);
        }

        // POST: CreditCard/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            creditcard creditcard = db.creditcard.Find(id);
            db.creditcard.Remove(creditcard);
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
