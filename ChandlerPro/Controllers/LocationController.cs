using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChandlerPro.Controllers
{
    class LocationController
    {
        public static async Task<List<Models.Location>> GetLocation(string locationName)
        {
            string url = Utils.LimbleConnection.SetUrl("/locations?name=" + locationName);
            HttpClient client = Utils.LimbleConnection.LimbleCredentials();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Models.Location>>(responseBody);
        }

        public static async Task<Models.Location> CreateLocation(string LocationName, string Timezone, string Phone)
        {
            Console.WriteLine("Creating a new location " + LocationName);
            Models.LocationPost post_data = new Models.LocationPost
            {
                name = LocationName,
                timezone = Timezone,
                phone = Phone
            };
            var json = JsonConvert.SerializeObject(post_data);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = Utils.LimbleConnection.SetUrl("/locations");
            HttpClient client = Utils.LimbleConnection.LimbleCredentials();
            try
            {
                var response = await client.PostAsync(url, data);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Location created...");
                return JsonConvert.DeserializeObject<Models.Location>(responseBody);
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine("Error creating location " + httpEx.Message);
                Models.Location error = new Models.Location
                {
                    Error = true,
                    Message = httpEx.Message
                };
                Console.WriteLine(error.Message);
                return error;
            }
        }

        public static async Task<Models.Location> UseLocation()
        {
            string locationName = ConfigurationManager.AppSettings["locationName"];
            List<Models.Location> getLocation = await GetLocation(locationName);
            if (getLocation.Count > 0)
            {
                Console.WriteLine("Location found!" + getLocation[0].LocationID);
                return getLocation[0];
            }
            else
            {
                string timezone = ConfigurationManager.AppSettings["locationTimezone"];
                string phone = ConfigurationManager.AppSettings["locationPhone"];
                Models.Location addLocation = await CreateLocation(locationName, timezone, phone);
                return addLocation;
            }
        }
    }
}
