using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_Giris.Models
{
    public class AdressViewModel
    {
        public int StudentID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
}