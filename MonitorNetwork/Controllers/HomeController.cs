using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonitorNetwork.Database;
using MonitorNetwork.Models;
using MonitorNetwork.BLL;
using System.Net;

namespace MonitorNetwork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MNDatabase context = new MNDatabase();
            NetworkModel nm = new NetworkModel();
            nm.transactions = context.transaction.Where(x => x.isEncrypted || x.isSent).AsEnumerable();

            return View(nm);
        }

        public ActionResult Send(int? id)
        {
            MNDatabase context = new MNDatabase();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = context.transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }

            transaction.isSent = true;

            context.SaveChanges();

            NetworkModel nm = new NetworkModel();
            nm.transactions = context.transaction.Where(x => x.isEncrypted || x.isSent).AsEnumerable();


            return PartialView("_TransactionPartial", nm.transactions);
        }

        public ActionResult EntireDatabase()
        {
            MNDatabase context = new MNDatabase();

            EntireDatabase edb = new EntireDatabase();

            edb.accounts = context.account.AsEnumerable();
            edb.creditcards = context.creditcard.AsEnumerable();
            edb.relays = context.relay.AsEnumerable();
            edb.relayconnectionweights = context.relayconnectionweight.AsEnumerable();
            edb.stores = context.store.AsEnumerable();
            edb.transaction = context.transaction.AsEnumerable();
            edb.user = context.user.AsEnumerable();

            return View(edb);
        }
    }
}