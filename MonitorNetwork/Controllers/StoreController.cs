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
            var selectList = from region in db.region
                             select new { regionID = region.regionID, regionColor = region.colors.colorName };

            ViewBag.store = new { regionID = new SelectList(selectList, "regionID", "regionColor") };
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
			if (!storeIPOkay(storeModel.store))
			{
				ModelState.AddModelError("store.storeIP", "IP already exists");
			}
			if (storeModel.checkboxRelayModel.Where(x => x.selected).Count() < 1)
			{
				ModelState.AddModelError("", "Needs to be connected to a relay");
			}
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

            var selectList = from region in db.region
                             select new { regionID = region.regionID, regionColor = region.colors.colorName };

            ViewBag.store = new { regionID = new SelectList(selectList, "regionID", "regionColor") };
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

		public bool storeIPOkay(store storeToCheck)
		{
			var stores = db.store.Where(x => x.storeIP == storeToCheck.storeIP);

			if (stores.Count() == 0)
			{

				var relays = db.relay.Where(x => x.relayIP == storeToCheck.storeIP);

				if (relays.Count() == 0)
				{
					return true;
				}
				return false;
			}
			return false;
		}
	}
}
