using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChandlerPro.Models
{

    //Company	OpenLine	PONUM	POLine	LineDesc	UnitCost	PartNum	CommentText	ClassID
    public class PODtl
    {
        public string Company { get; set; }
        public string OpenLine { get; set; }
        public string PONUM { get; set; }
        public string POLine { get; set; }
        public string LineDesc { get; set; }
        public string UnitCost { get; set; }
        public string CommentText { get; set; }
        public string ClassID { get; set; }
        public string PartNum { get; set; }
        public string BaseQt { get; set; }
        public string OrderQty { get; set; }
        public string TotalTax { get; set; }

    }

    public class RootPODtl
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }
        public List<PODtl> value { get; set; }
    }
}
