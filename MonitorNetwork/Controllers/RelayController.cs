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
	[CheckAuthorization]
	public class RelayController : Controller
    {
        private MNDatabase db = new MNDatabase();

        // GET: Store
        public ActionResult Index()
        {
            var relay = db.relay.Include(s => s.region);
            return View(relay.ToList());
        }

        // GET: Store/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            relay relay = db.relay.Find(id);
            if (relay == null)
            {
                return HttpNotFound();
            }
            return View(relay);
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            var selectList = from region in db.region
                             select new { regionID = region.regionID, regionColor = region.colors.colorName };

            ViewBag.relay = new { regionID = new SelectList(selectList, "regionID", "regionColor") };
            RelayModel relayModel = new RelayModel();

            relayModel.checkboxRelayModel = GetCheckboxRelays(db.region.First().regionID);
			relayModel.checkboxStoreModel = GetCheckboxStores(db.region.First().regionID);
            return View(relayModel);
        }

		// POST: Store/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		public ActionResult Create(RelayModel relayModel)
		{
			
			if (!relayIPOkay(relayModel.relay))
			{
				ModelState.AddModelError("relay.relayIP", "IP already exists");
			}

			if (relayModel.checkboxRelayModel.Where(x => x.selected).Count() <= 0)
			{
				ModelState.AddModelError("", "Must select at least one relay connection");
			}
			if (relayModel.checkboxStoreModel.Where(x => x.selected).Count() <= 0)
			{
				if (relayModel.checkboxRelayModel.Where(x => x.selected).Count() <= 1)
				{
					ModelState.AddModelError("", "Incorrect number of connections");
				}
			}
			if (ModelState.IsValid)
			{
				db.relay.Add(relayModel.relay);

				db.SaveChanges();

				var selectedRelays = relayModel.checkboxRelayModel.Where(x => x.selected);
				foreach (var selectedRelay in selectedRelays)
				{
					connections connection = new connections()
					{
						relayID = relayModel.relay.relayID,
						destRelayID = selectedRelay.relayID,
						isActive = true,
						weight = selectedRelay.weight
					};

					db.connections.Add(connection);
				}

				var selectedStores = relayModel.checkboxStoreModel.Where(x => x.selected);
				foreach (var selectedStore in selectedStores)
				{
					connections connection2 = new connections()
					{
						storeID = selectedStore.storeID,
						destRelayID = relayModel.relay.relayID,
						isActive = true,
						weight = selectedStore.weight
					};

					db.connections.Add(connection2);
				}

				db.SaveChanges();

				return RedirectToAction("Index", "Relay");
			}
			

			relayModel.checkboxRelayModel = GetCheckboxRelays(relayModel.relay.regionID);
			relayModel.checkboxStoreModel = GetCheckboxStores(relayModel.relay.regionID);

			var selectList = from region in db.region
							 select new { regionID = region.regionID, regionColor = region.colors.colorName };


			ViewBag.relay = new { regionID = new SelectList(selectList, "regionID", "regionColor") };

            return View(relayModel);
        }

        [HttpGet]
        public ActionResult GetRelays(int regionId)
        {
            return PartialView("_RelayPartial", new RelayModel() {
                checkboxRelayModel = GetCheckboxRelays(regionId)
            });
        }

		[HttpGet]
		public ActionResult GetStores(int regionId)
		{
			return PartialView("_StorePartial", new RelayModel()
			{
				checkboxStoreModel = GetCheckboxStores(regionId)
			});
		}

		private IList<CheckboxRelayModel> GetCheckboxRelays(int regionId)
        {
            var region = db.region.FirstOrDefault(x => x.regionID == regionId);

            return (from relay in region.relay
                   select new CheckboxRelayModel
                   {
                       selected = false,
                       relayIP = relay.relayIP,
                       relayID = relay.relayID
                   }).ToList();
        }

		private IList<CheckboxStoreModel> GetCheckboxStores(int regionId)
		{
			var region = db.region.FirstOrDefault(x => x.regionID == regionId);

			return (from store in region.store
					select new CheckboxStoreModel
					{
						selected = false,
						storeIP = store.storeIP,
						storeID = store.storeID,
						merchantName = store.merchantName
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

		public bool relayIPOkay(relay relayToCheck)
		{
			var stores = db.store.Where(x => x.storeIP == relayToCheck.relayIP);

			if (stores.Count() == 0)
			{
				var relays = db.relay.Where(x => x.relayIP == relayToCheck.relayIP);

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
