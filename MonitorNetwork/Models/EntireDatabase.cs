using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonitorNetwork.Database;

namespace MonitorNetwork.Models
{
    public class EntireDatabase
    {

        public IEnumerable<account> accounts;
        public IEnumerable<creditcard> creditcards;
        public IEnumerable<relay> relays;
		public IEnumerable<region> regions;
        public IEnumerable<store> stores;
        public IEnumerable<transaction> transaction;
        public IEnumerable<user> user;
        public IEnumerable<connections> connections;
    }
}