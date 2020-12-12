using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourService.Model
{
    public class BestTours
    {
        public BestTours(Hotel hotel, TourProduct tour)
        {
            Id = hotel.Id;
            Name = hotel.Name;
            Country = hotel.Country;
            Price = tour.Price;
            CheckInDate = tour.CheckInDate;
            Nights = tour.Nights;
            Meals = tour.Meal;
            Adults = tour.TouristGroup.Adults;
            Kids = tour.TouristGroup.Kids;
            Resort = hotel.Resort;
        }

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }
        [JsonProperty("checkInDate")]
        public string CheckInDate { get; set; }
        [JsonProperty("meals")]
        public string Meals { get; set; } 
        [JsonProperty("nights")]
        public int Nights { get; set; }
        [JsonProperty("resort")]
        public string Resort { get; set; }
        [JsonProperty("departureCity")]
        public string DepartureCity { get; set; } = "Москва";
        [JsonProperty("nights")]
        public int Adults { get; set; }
        [JsonProperty("nights")]
        public int Kids { get; set; }
    }
}
