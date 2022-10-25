using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChandlerPro.Models
{
    class Location
    {
        [JsonPropertyName("locationID")]
        public string LocationID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        public bool Error { get; set; }
        public string Message { get; set; }
    }

    class LocationPost
    {
        public string name { get; set; }
        public string timezone { get; set; }
        public string phone { get; set; }
    }
}
