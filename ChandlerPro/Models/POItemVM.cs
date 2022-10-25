using System;
using System.Collections.Generic;
using System.Text;

namespace ChandlerPro.Models
{
    public class POItemVM
    {
        public int itemType { get; set; }
        //public string? name { get; set; }
        public string? description { get; set; }
        public int partID { get; set; }
        public int? quantity { get; set; }
        public float? rate { get; set; }
        public float? tax { get; set; }

    }
}
