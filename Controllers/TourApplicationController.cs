using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TourService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace TourService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourApplcationController : ControllerBase
    {
        ConfigApplication options;
        ITourApplication tourApplication;
        public TourApplcationController(IOptions<ConfigApplication> options, ITourApplication tourApplication)
        {
            this.options = options.Value;
            this.tourApplication = tourApplication;
        }
        public async Task<IActionResult> GetAsync()
        {
            await tourApplication.DownloadCountriesAsync();
            await tourApplication.DownloadToursAsync(options.HotUrl);
            return new OkObjectResult(tourApplication.GetHotTravels());
        }
    }
}