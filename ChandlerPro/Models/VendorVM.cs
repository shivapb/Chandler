using System;
using System.Collections.Generic;
using System.Text;

namespace ChandlerPro.Models
{
    public class VendorVM
    {
        public string locationID { get; set; }
        public string name { get; set; }
        public string? email { get; set; } = null;
        public string? phone { get; set; } = null;
        public string? contact { get; set; } = null;
    }
}
