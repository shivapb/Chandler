using ChandlerPro.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChandlerPro.Controllers
{
    public class VendorController
    {
        public static async Task<int> NewVendorID(VendorVM vendorInfo, string EpicorVenderVendorID)
        {
            var json = JsonConvert.SerializeObject(vendorInfo);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = Utils.LimbleConnection.SetUrl("/vendors");
            HttpClient client = Utils.LimbleConnection.LimbleCredentials();
            try
            {
                var response = await client.PostAsync(url, data);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                string LimbleVenderID = JObject.Parse(responseBody)["vendorID"].ToString();


                SaveVendorInfo(EpicorVenderVendorID, Convert.ToInt32(LimbleVenderID));
                return Convert.ToInt32(LimbleVenderID);
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine(httpEx.Message);
                return 0;
            }
        }


        public static void SaveVendorInfo(string epicorVendorVendorID, int limbleVenderVendorID)
        {
            var fields = Utils.LimbleMongoDB.Connection().GetCollection<BsonDocument>("Vendors");
            var doc = new BsonDocument
                {
                    {"epicorVendorVendorID", epicorVendorVendorID },
                    {"LimbleVendorVendorID", limbleVenderVendorID }
                };

            fields.InsertOne(doc);
        }

       

        //START : MongoDB save means Local removed duplicate VendorName 
        public static async Task<int> getVendorID(string vendorvendorID)
        {
            var fields = Utils.LimbleMongoDB.Connection().GetCollection<BsonDocument>("Vendors");  //Table Name 
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("epicorVendorVendorID", vendorvendorID);
            var doc = fields.Find(filter).ToList();
            try
            {
                if (doc.Count != 0)
                {

                    int LimbleVendorVendorID = doc.FirstOrDefault().GetValue("LimbleVendorVendorID", new BsonString(string.Empty)).AsInt32;
                    return LimbleVendorVendorID;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

    }
}
