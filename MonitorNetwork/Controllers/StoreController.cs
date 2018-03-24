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
            ViewBag.store = new { regionID = new SelectList(db.region, "regionID", "regionColor") };
            StoreModel storeModel = new StoreModel();

            storeModel.checkboxRelayModel = GetCheckboxRelays(db.region.First().regionID);
            return View(storeModel);
        }

        // POST: Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(StoreModel storeModel)
        {
            if (ModelState.IsValid)
            {
                db.store.Add(storeModel.store);

                db.SaveChanges();

                var selectedRelays = storeModel.checkboxRelayModel.Where(x => x.selected);
                foreach (var selectedRelay in selectedRelays)
                {
                    connections connection = new connections()
                    {
                        storeID = storeModel.store.storeID,
                        destRelayID = selectedRelay.relayID,
                        isActive = true,
                        weight = selectedRelay.weight
                    };

                    db.connections.Add(connection);
                }

                db.SaveChanges();

                return RedirectToAction("Index", "Store");
            }

            storeModel.checkboxRelayModel = GetCheckboxRelays(storeModel.store.regionID);

            ViewBag.store = new { regionID = new SelectList(db.region, "regionID", "regionColor", storeModel.store.regionID) };
            return View(storeModel);
        }

        [HttpGet]
        public ActionResult GetRelays(int regionId)
        {
            return PartialView("_RelayPartial", new StoreModel() {
                checkboxRelayModel = GetCheckboxRelays(regionId)
            });
        }

        private IList<CheckboxRelayModel> GetCheckboxRelays(int regionId)
        {
            var region = db.region.FirstOrDefault(x => x.regionID == regionId);

            return (from relay in region.relay
                    where !relay.isProcessingCenter
                   select new CheckboxRelayModel
                   {
                       selected = false,
                       relayIP = relay.relayIP,
                       relayID = relay.relayID
                   }).ToList();
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
