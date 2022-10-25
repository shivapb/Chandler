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
    public class PurchaseOrderController
    {
      
        public static async Task<int> NewPurchaseOrder(lmblMinPOHeader objlmblMinPOHeader)
        {
            Models.PurchaseOrderVM post_data = new Models.PurchaseOrderVM
            {
                locationID = objlmblMinPOHeader.locationID,
                vendorID = objlmblMinPOHeader.vendorID,
                userID = objlmblMinPOHeader.userID,
                //taskID = objlmblMinPOHeader.taskID,
                //budgetID = objlmblMinPOHeader.budgetID,
                date = objlmblMinPOHeader.date,
                expectedDate = objlmblMinPOHeader.expectedDate,
                requestDescription = objlmblMinPOHeader.requestDescription.ToString()

            };
            var json = JsonConvert.SerializeObject(post_data);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = Utils.LimbleConnection.SetUrl("/po");
            HttpClient client = Utils.LimbleConnection.LimbleCredentials();
            try
            {
                var response = await client.PostAsync(url, data);
                response.EnsureSuccessStatusCode();
                if(response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    responseBody = JObject.Parse(responseBody)["poID"].ToString();
                    return Convert.ToInt32(responseBody);
                }
                else
                {

                    HttpResponseMessage urlContents = response;
                    var content = urlContents.Content.ReadAsStringAsync();
                    var res = Task.FromResult(content.Result);
                    var errorMessage = JObject.Parse(res.Result)["raw"]["description (body)"]["message"].ToString();
                    Console.WriteLine("Error creating NewPurchaseOrder " + errorMessage);
                    return -999;
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine("Error creating NewPurchaseOrder " + httpEx.Message);
                return 0;
            }
        }

    }
}
