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

            nm.connections = (from conn in context.connections
                              select new Connections
                              {
                                  connID = conn.connID,
                                  storeID = conn.storeID,
                                  relayID = conn.relayID,
                                  destRelayID = conn.destRelayID,
                                  weight = conn.weight,
                                  active = conn.active
                              }).ToList();

            nm.relays = (from relay in context.relay
                         select new Relays
                         {
                             relayID = relay.relayID,
                             relayIP = relay.relayIP,
                             status = relay.status,
                             isProcessingCenter = relay.isProcessingCenter
                         }).ToList();

            nm.stores = (from store in context.store
                         select new Stores
                         {
                             storeID = store.storeID,
                             storeIP = store.storeIP,
                             merchantName = store.merchantName
                         }).ToList();

            nm.cytoscapeNodes = (from relay in context.relay
                        select new CytoscapeData
                        {
                            data = new CytoscapeNode()
                            {
                                id = "R" + relay.relayID,
                                label = relay.relayIP.Substring(8)
                            }
                        })
                        .Concat(from store in context.store
                                select new CytoscapeData
                                {
                                    data = new CytoscapeNode()
                                    {
                                        id = "S" + store.storeID,
                                        label = store.storeIP.Substring(8)
                                    }
                                }).ToList();

            nm.cytoscapeEdges = (from conn in context.connections
                                 select new CytoscapeData
                                 {
                                     data = new CytoscapeEdge()
                                     {
                                         id = conn.relayID.HasValue ? "R" + conn.relayID + "R" + conn.destRelayID : "S" + conn.storeID + "R" + conn.destRelayID,
                                         weight = conn.weight,
                                         source = conn.relayID.HasValue ? "R" + conn.relayID : "S" + conn.storeID,
                                         target = "R" + conn.destRelayID
                                     }
                                 }).ToList();

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
            edb.stores = context.store.AsEnumerable();
            edb.transaction = context.transaction.AsEnumerable();
            edb.user = context.user.AsEnumerable();
            edb.connections = context.connections.AsEnumerable();

            return View(edb);
        }
    }
}