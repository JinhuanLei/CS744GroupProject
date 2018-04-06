using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MonitorNetwork.Models
{
    public class LoginModel
    {
        public string username { get; set; }

        public string password { get; set; }

        public string returnURL { get; set; }

    }

    public class SecurityQuestion
    {
        public string username { get; set; }

        public IList<int> askedQuestions { get; set; } = new List<int>();

        public string question { get; set; }

        [Required]
        public string answer { get; set; }

        public string returnURL { get; set; }
    }
}