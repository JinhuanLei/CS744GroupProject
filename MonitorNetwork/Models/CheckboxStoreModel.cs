using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MonitorNetwork.Models
{
    public class CheckboxStoreModel
    {
        [Required]
        public int storeID { get; set; }

        public string storeIP { get; set; }

		public bool selected { get; set; }

		public string merchantName { get; set; }


		[Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter a value equal to or greater than 0")]
        public int weight { get; set; }
    }
}