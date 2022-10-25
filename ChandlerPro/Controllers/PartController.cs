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
    public class PartController
    {
        public static async Task<int> NewPartID(PartVM partInfo, string EpicorPartID)
        {
            var json = JsonConvert.SerializeObject(partInfo);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = Utils.LimbleConnection.SetUrl("/parts/");
            HttpClient client = Utils.LimbleConnection.LimbleCredentials();
            try
            {
                var response = await client.PostAsync(url, data);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                string LimblepartID = JObject.Parse(responseBody)["partID"].ToString();


                SavePartInfo(EpicorPartID, Convert.ToInt32(LimblepartID));
                return Convert.ToInt32(LimblepartID);
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine(httpEx.Message);
                return 0;
            }
        }


        public static void SavePartInfo(string epicorpartID, int limblepartID)
        {
            var fields = Utils.LimbleMongoDB.Connection().GetCollection<BsonDocument>("Parts");
            var doc = new BsonDocument
                {
                    {"epicorPartID", epicorpartID },
                    {"LimblePartID", limblepartID }
                };

            fields.InsertOne(doc);
        }

       

        //START : MongoDB save means Local removed duplicate VendorName 
        public static async Task<int> getPartID(string partID)
        {
            var fields = Utils.LimbleMongoDB.Connection().GetCollection<BsonDocument>("Parts");  //Table Name 
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("epicorPartID", partID);
            var doc = fields.Find(filter).ToList();
            try
            {
                if (doc.Count != 0)
                {
                    int LimblePartID = doc.FirstOrDefault().GetValue("LimblePartID", new BsonString(string.Empty)).AsInt32;
                    return LimblePartID;
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
