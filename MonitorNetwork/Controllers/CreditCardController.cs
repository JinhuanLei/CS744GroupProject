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
    public class CreditCardController : Controller
    {
        private MNDatabase db = new MNDatabase();

        // GET: CreditCard
        public ActionResult Index()
        {
            var creditcard = db.creditcard.Include(c => c.account);
            return View(creditcard.ToList());
        }

        // GET: CreditCard/Create
        public ActionResult Create()
        {
           GenerateCreditCard creditCardGenerator = new GenerateCreditCard(db);
            ViewBag.creditcard = new { accountID = new SelectList((from acct in db.account select new { accountID = acct.accountID, fullname = acct.accountFirstName + " " + acct.accountLastName }), "accountID", "fullname") };
            ViewBag.creditCardNumber = creditCardGenerator.GetValidUnusedCreditCard();

            return View();
        }

        // POST: CreditCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(CreditCardAndAccountViewModel creditCardAndAccount)
        {
            if (creditCardAndAccount.existing)
            {
                foreach (var key in ModelState.Keys.Where(x => x.StartsWith("account.")))
                {
                    ModelState[key].Errors.Clear();
                }
            }

            if (ModelState.IsValid)
            {
                if(creditCardAndAccount.existing)
                {
                    db.creditcard.Add(creditCardAndAccount.creditcard);
                } else
                {
                    creditCardAndAccount.account.creditcard.Add(creditCardAndAccount.creditcard);
                    db.account.Add(creditCardAndAccount.account);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            GenerateCreditCard creditCardGenerator = new GenerateCreditCard(db);

            ViewBag.creditcard = new { accountID = new SelectList((from acct in db.account select new { accountID = acct.accountID, fullname = acct.accountFirstName + " " + acct.accountLastName }), "accountID", "fullname", creditCardAndAccount.creditcard.accountID) };
            ViewBag.creditCardNumber = creditCardGenerator.GetValidUnusedCreditCard();

            return View(creditCardAndAccount);
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
            return View(creditcard);
        }

        [HttpPost]
        public ActionResult DeleteCheckAccount(int? id)
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

            account creditCardAccount = creditcard.account;

            if (creditCardAccount.creditcard.Count == 1)
            {
                if(creditCardAccount.balance > 0)
                {
                    return Json("LAST_CARD_NON_ZERO_BALANCE");
                }
                return Json("LAST_CARD");
            }

            return Json("DELETE");
        }

        // POST: CreditCard/Delete/5
        [HttpPost]
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

            account creditCardAccount = creditcard.account;

            db.creditcard.Remove(creditcard);

            if(creditCardAccount.creditcard.Count == 0)
            {
                db.account.Remove(creditCardAccount);
            }

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
