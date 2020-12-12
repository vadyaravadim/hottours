using TourService.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourService
{
    public interface ITourApplication 
    {
        public Task DownloadToursAsync(string hotUrl);
        public IEnumerable<BestTours> GetHotTravels();
        public Task DownloadCountriesAsync();
        //public void MappingHotels(IEnumerable<TourProduct> bestTravels);
    }
}
