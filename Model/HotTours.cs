using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TourService.Model
{
    public class HotTours
    {
        [JsonProperty("tourProducts")]
        public List<TourProduct> TourProducts { get; set; }
        [JsonProperty("hotels")]
        public List<Hotel> Hotels { get; set; }
    }

    public class Hotel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("resort")]
        public string Resort { get; set; }
        [JsonProperty("rating")]
        public string Rating { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
    }

    public class TourProduct
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("hotel")]
        public string Hotel { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }
        [JsonProperty("checkInDate")]
        public string CheckInDate { get; set; }
        [JsonProperty("originalPrice")]
        public string OriginalPrice { get; set; }
        [JsonProperty("nights")]
        public int Nights { get; set; }
        [JsonProperty("meal")]
        public string Meal { get; set; }
        [JsonProperty("departureCity")]
        public string DepartureCity { get; set; }
        [JsonProperty("touristGroup")]
        public TouristGroup TouristGroup { get; set; }
    }

    public class TouristGroup
    {
        [JsonProperty("adults")]
        public int Adults { get; set; }
        [JsonProperty("kids")]
        public int Kids { get; set; }
        [JsonProperty("infants")]
        public int Infants { get; set; }
    }
}
