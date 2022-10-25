using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChandlerPro.Models
{
    //Company	PONum	OrderDate	BuyerID	Approve	VendorVendorID
    public class POHeader
    {
        public string Company { get; set; }
        public string PONum { get; set; }
        public string OrderDate { get; set; }
        public string BuyerID { get; set; }
        public string Approve { get; set; }
        public string VendorVendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorCntEmailAddress { get; set; }
        public string VendorCntPhoneNum { get; set; }
        public string VendorPPPrimPCon { get; set; } 
    }

    public class RootPOHeader
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }
        public List<POHeader> value { get; set; }
    }

    

}
