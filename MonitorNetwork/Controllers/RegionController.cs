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
    public class RegionController : Controller
    {
        private MNDatabase db = new MNDatabase();

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

            ViewBag.region = new { colorID = new SelectList(temp, "colorID", "colorName") };

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
                    regionNumber = db.region.Select(x => x.regionID).Max() + 1
				};
				db.region.Add(newRegion);
				db.store.Add(regionStoreRelay.store);
				db.SaveChanges();


				relay gatewayRelay = new relay()
				{
					isActive = true,
					isProcessingCenter = false,
					queueLimit = regionStoreRelay.relay.queueLimit,
					isGateway = true,
					relayIP = regionStoreRelay.relay.relayIP,
					regionID = newRegion.regionID
				};

				db.relay.Add(gatewayRelay);
				db.SaveChanges();

				connections connection = new connections()
				{
					storeID = regionStoreRelay.store.storeID,
					destRelayID = gatewayRelay.relayID,
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
						relayID = gatewayRelay.relayID,
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

			ViewBag.region = new { colorID = new SelectList(temp, "colorID", "colorName") };
			return View(regionStoreRelay);
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
