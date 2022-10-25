using System;
using System.Collections.Generic;
using System.Text;

namespace ChandlerPro.Models
{
    public class PurchaseOrderVM
    {
        public int locationID { get; set; }
        public int vendorID { get; set; }
        public int userID { get; set; }
        //public int? taskID { get; set; }
        //public int? budgetID { get; set; }
        public int? date { get; set; }
        public int? expectedDate { get; set; }
        public string? requestDescription { get; set; }
        //public string? customField1 { get; set; }
        //public string? customField2 { get; set; }
        //public string? customField3 { get; set; }
        //public string? customField4 { get; set; }
        //public string? customField5 { get; set; }
        //public string? customField6 { get; set; }
        //public bool Error { get; set; }
        //public string Message { get; set; }
    }
}
