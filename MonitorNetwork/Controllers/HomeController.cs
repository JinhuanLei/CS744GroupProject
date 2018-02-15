using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonitorNetwork.Database;
using MonitorNetwork.Models;

namespace MonitorNetwork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			MNDatabase context = new MNDatabase();
			NetworkModel nm = new NetworkModel();
			nm.transactions = context.transaction.ToList();
			nm.connections = context.connections.ToList();
			nm.relays = context.relay.ToList();
			return View(nm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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