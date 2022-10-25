using System;
using System.Collections.Generic;
using System.Text;

namespace ChandlerPro.Models
{
    // lmblPODtl myDeserializedClass = JsonConvert.DeserializeObject<lmblPODtl>(myJsonResponse);
    public class lmblPODtl
    {
        public int itemType { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int glID { get; set; }
       // public int taskID { get; set; }
       // public int assetID { get; set; }
        public int partID { get; set; }
        public int quantity { get; set; }
        public float rate { get; set; }
        public float tax { get; set; }
        public int discount { get; set; }
        public double shipping { get; set; }
        
    }
}
