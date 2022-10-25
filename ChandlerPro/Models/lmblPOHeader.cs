using System;
using System.Collections.Generic;
using System.Text;

namespace ChandlerPro.Models
{
    // lmblPOHeader myDeserializedClass = JsonConvert.DeserializeObject<List<lmblPOHeader>>(myJsonResponse);
    public class Meta
    {
        public string items { get; set; }
        public string budget { get; set; }
        public string budgetSteps { get; set; }
    }

    public class lmblPOHeader
    {
        public int poID { get; set; }
        public int budgetID { get; set; }
        public int poNumber { get; set; }
        public int vendorID { get; set; }
        public int locationID { get; set; }
        public int userIDStarted { get; set; }
        public int userID { get; set; }
        public int teamID { get; set; }
        public int requestedByUserID { get; set; }
        public int date { get; set; }
        public string expectedDate { get; set; }
        public string notesToVendor { get; set; }
        public string billTo { get; set; }
        public string shipTo { get; set; }
        public int status { get; set; }
        public string customField1 { get; set; }
        public string customField2 { get; set; }
        public string customField3 { get; set; }
        public string customField4 { get; set; }
        public string customField5 { get; set; }
        public string customField6 { get; set; }
        public int lastEdited { get; set; }
        public string poPrefix { get; set; }
        public Meta meta { get; set; }
    }


    public class lmblMinPOHeader
    {
        public int locationID { get; set; }
        public int vendorID { get; set; }
        public int userID { get; set; }
        //public int taskID { get; set; }
        public string requestDescription { get; set; }
        public int date { get; set; }
        public int expectedDate { get; set; }
        //public int budgetID { get; set; }
        //public string customField1 { get; set; }
        //public string customField2 { get; set; }
        //public string customField3 { get; set; }
        //public string customField4 { get; set; }
        //public string customField5 { get; set; }
        //public string customField6 { get; set; }
    }



}
