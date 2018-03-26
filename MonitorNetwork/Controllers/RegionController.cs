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
    public class RegionController : Controller
    {
        private MNDatabase db = new MNDatabase();

        // GET: Region
        public ActionResult Index()
        {
            return View(db.region.ToList());
        }

        // GET: Region/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            region region = db.region.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

		// GET: Region/Create
		public ActionResult Create()
		{
			RegionStoreRelayModel regionStoreRelay = new RegionStoreRelayModel();

			regionStoreRelay.CheckboxGatewayModel = GetGatewayRelays();

			var colorInfo = from colors in db.colors
							select new { colors.colorID };

			var colorsUsed = from region in db.region
							 select new { region.colorID };

			var temp = from colors in colorInfo.Except(colorsUsed)
					   join colors2 in db.colors on colors.colorID equals colors2.colorID
					   select new { colors2.colorName, colors2.colorID };

			ViewBag.colorID = new SelectList(temp, "colorID", "colorName");

			return View(regionStoreRelay);
		}
		
        // POST: Region/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(RegionStoreRelayModel regionStoreRelay)
        {
            if (ModelState.IsValid)
            {
				region newRegion = new region()
				{
					colorID = regionStoreRelay.region.colorID,
					regionNumber = regionStoreRelay.region.regionID
				};
				db.region.Add(regionStoreRelay.region);
				db.store.Add(regionStoreRelay.store);
				db.SaveChanges();

				relay gatewayRelay = new relay()
				{
					isActive = true,
					isProcessingCenter = false,
					queueLimit = regionStoreRelay.relay.queueLimit,
					isGateway = true,
					relayIP = regionStoreRelay.relay.relayIP,
					regionID = regionStoreRelay.relay.regionID
				};

				db.relay.Add(gatewayRelay);
				
				
				var gateways = db.relay.Where(x => x.isGateway || x.isProcessingCenter);
                db.SaveChanges();

				connections connection = new connections()
				{
					storeID = regionStoreRelay.store.storeID,
					destRelayID = regionStoreRelay.relay.relayID,
					isActive = true,
					weight = regionStoreRelay.connections.weight
				};

				db.connections.Add(connection);
				db.SaveChanges();

				var selectedRelays = regionStoreRelay.CheckboxGatewayModel.Where(x => x.selected);
				foreach (var selectedRelay in selectedRelays)
				{
					connections connection2 = new connections()
					{
						relayID = regionStoreRelay.relay.relayID,
						destRelayID = selectedRelay.relayID,
						isActive = true,
						weight = selectedRelay.weight
					};

					db.connections.Add(connection2);
				}

				db.SaveChanges();
				return RedirectToAction("Index", "Home");
            }

			regionStoreRelay.CheckboxGatewayModel = GetGatewayRelays();

			var colorInfo = from colors in db.colors
							select new { colors.colorID };

			var colorsUsed = from region in db.region
							 select new { region.colorID };

			var temp = from colors in colorInfo.Except(colorsUsed)
					   join colors2 in db.colors on colors.colorID equals colors2.colorID
					   select new { colors2.colorName, colors2.colorID };

			ViewBag.colorID = new SelectList(temp, "colorID", "colorName");
			return View(regionStoreRelay);
        }

		[HttpGet]
		public ActionResult GetRelays(int regionId)
		{
			return PartialView("_GatewayPartial", new RegionStoreRelayModel()
			{
				CheckboxGatewayModel = GetGatewayRelays()
			});
		}

		// GET: Region/Edit/5
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            region region = db.region.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: Region/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "regionID,regionNumber")] region region)
        {
            if (ModelState.IsValid)
            {
                db.Entry(region).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(region);
        }

        // GET: Region/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            region region = db.region.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: Region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            region region = db.region.Find(id);
            db.region.Remove(region);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		private IList<CheckboxGatewayModel> GetGatewayRelays()
		{
			//var region = db.region.FirstOrDefault(x => x.regionID == regionId);
			var gateways = db.relay.Where(x => x.isGateway || x.isProcessingCenter);

			return (from relay in gateways
					select new CheckboxGatewayModel
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
