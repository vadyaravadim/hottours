using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourService.Model
{
    public class Countries
    {
       [JsonProperty("data")]
       public List<DataCountries> DataCountries { get; set; }
    }

    public class DataCountries
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("popular")]
        public int Popular { get; set; }
    }
}
