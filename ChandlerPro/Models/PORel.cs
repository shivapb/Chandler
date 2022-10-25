using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChandlerPro.Models
{
    //Company	OpenRelease	PONum	POLine	PORelNum	DueDate	RelQty	Plant	TranType	ReceivedQty
    public class PORel
    {
        public string Company { get; set; }
        public string OpenRelease { get; set; }
        public string PONum { get; set; }
        public string POLine { get; set; }
        public string PORelNum { get; set; }
        public string DueDate { get; set; }
        public string RelQty { get; set; }
        public string Plant { get; set; }
        public string TranType { get; set; }
        public string ReceivedQty { get; set; }

    }

    public class RootPORel
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }
        public List<PORel> value { get; set; }
    }

}
