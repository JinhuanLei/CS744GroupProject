using MonitorNetwork.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonitorNetwork.Controllers
{
    public class QueueController : Controller
    {
        private MNDatabase db = new MNDatabase();

        // GET: Queue
        public ActionResult Index()
        {
            List<relay> relays = db.relay.ToList();
            List<relay> relaylist = new List<relay>();
            foreach (var r in relays)
            {
                if (!r.isActive)
                {
                    relaylist.Add(r);
                }
            }
                return View(relaylist);
           
        }


        [HttpPost]
        public ActionResult setQueueLimit(relay relay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(relay);
        }
    }


   
}