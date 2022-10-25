using ChandlerPro.Models;
using ChandlerPro.Utils;
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
    public class POItemController
    {
        public static async Task<int> NewPOItem(lmblPODtl lmblPODtl, int PurchaseOrderID)
        {
            Models.POItemVM post_data = new Models.POItemVM
            {
                itemType = lmblPODtl.itemType,
                description = lmblPODtl.description,
                rate = lmblPODtl.rate,
                tax = lmblPODtl.tax,
                quantity = lmblPODtl.quantity,
                partID = lmblPODtl.partID

            };

            var newJson = ReClasser.ModifyJsonObject(post_data);
            var json = JsonConvert.SerializeObject(newJson);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = Utils.LimbleConnection.SetUrl("/po/" + PurchaseOrderID + "/items");
            HttpClient client = Utils.LimbleConnection.LimbleCredentials();
            try
            {
                var response = await client.PostAsync(url, data);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    string result = JObject.Parse(responseBody)["itemID"].ToString();
                    return Convert.ToInt32(result);
                }
                else
                {
                    HttpResponseMessage urlContents =  response;
                    var content = urlContents.Content.ReadAsStringAsync();
                    var res = Task.FromResult(content.Result);
                    var errorMessage = JObject.Parse(res.Result)["raw"]["description (body)"]["message"].ToString();
                    Console.WriteLine("Error creating PurchaseOrderItem "+errorMessage);
                    return -999;
                }    
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine("Error creating PurchaseOrderItem " + httpEx.Message);
                return 0;
            }
        }

    }

    
}
