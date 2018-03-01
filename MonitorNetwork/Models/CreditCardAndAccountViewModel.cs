using MonitorNetwork.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitorNetwork.Models
{
    public class CreditCardAndAccountViewModel
    {
        public creditcard creditcard { get; set; }
        public account account { get; set; }

        public bool existing { get; set; }
    }
}