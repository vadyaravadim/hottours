using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TourService.Model;
using Newtonsoft.Json;
using System.Text;

namespace TourService
{
    public class TourApplication : ITourApplication
    {
        public HttpClient Client { get; set; }
        public HashSet<HotTours> Travels { get; set; } = new HashSet<HotTours>();
        private List<DataCountries> Countries { get; set; }
        public IEnumerable<BestTours> BestTours { get; set; } = new List<BestTours>();
        public Random RandomWidgetId { get; set; } = new Random();

        public TourApplication(HttpClient client)
        {
            Client = client;
        }
        #region Download Data
        public async Task DownloadCountriesAsync()
        {
            HttpResponseMessage httpResponse = await Client.GetAsync("http://api-gateway.travelata.ru/directory/countries");
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(await httpResponse.Content.ReadAsStringAsync());
            }
            Countries countries = JsonConvert.DeserializeObject<Countries>(await httpResponse.Content.ReadAsStringAsync());
            // Берем все кроме России
            Countries = countries.DataCountries.Where(e => e.Id != 76).Select(e => e).ToList();
            if (Countries.Count == 0)
            {
                throw new Exception("countries is null!");
            }
        }
        public async Task DownloadToursAsync(string hotUrl)
        {
            IEnumerable<List<int>> countriesPartition = Countries.Select(e => e.Id).ToList().Partition(3);

            IEnumerable<Task> tasks = countriesPartition.Select(async countries =>
            {
                await Task.Run(async () =>
                {
                    foreach(int country in countries)
                    {
                        int widgedId = RandomWidgetId.Next(1000000000, 2146662341);

                        StringBuilder sbHotUrl = new StringBuilder(hotUrl);

                        sbHotUrl.Replace("0000000000", widgedId.ToString());
                        sbHotUrl.Insert(sbHotUrl.Length, country);
                        //sbHotUrl.Insert(sbHotUrl.Length, $"&checkInDateRange[from]={DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")}&checkInDateRange[to]={DateTime.Now.AddDays(2).AddMonths(2).ToString("yyyy-MM-dd")}");
                        
                        Console.WriteLine(sbHotUrl.ToString());
                        HttpResponseMessage httpResponse = await Client.GetAsync(sbHotUrl.ToString());
                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            Console.WriteLine(await httpResponse.Content.ReadAsStringAsync());
                        }
                        StringBuilder hotContent = new StringBuilder(await httpResponse.Content.ReadAsStringAsync());
                        hotContent.Replace($"_tatData.items['{widgedId}'].data =", "");
                        hotContent.Remove(hotContent.Length - 2, 2);
                        HotTours hotTours = JsonConvert.DeserializeObject<HotTours>(hotContent.ToString());
                        Travels.Add(hotTours);
                    }
                });
            });

            await Task.WhenAll(tasks);
        }
        #endregion
        public IEnumerable<BestTours> GetHotTravels()
        {
            MappingHotels();
            
            var toursGroupsByCountry = BestTours.GroupBy(e => e.Country);

            var top = toursGroupsByCountry.Select(e => e.OrderBy(e => e.Price).FirstOrDefault());
            
            return top.OrderBy(e => e.Price).Take(10);
        }

        public void MappingHotels()
        {
            BestTours = from TourProduct tour in Travels.SelectMany(travel => travel.TourProducts.Select(tour => tour))
                        where tour != null
                        join Hotel hotel in Travels.SelectMany(travel => travel.Hotels.Select(hotel => hotel)) on tour.Hotel equals hotel.Id
                        select new BestTours(hotel, tour);
        }
    }
}
