using Microsoft.AspNetCore.Mvc;
using Mirea_Avia.Models;
using Newtonsoft.Json;
using System.Diagnostics;


namespace Mirea_Avia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public static readonly string TOKEN = "d776e6b299669e2e28eeac8d826333c0";

        public static readonly string BASE_URL = "https://api.travelpayouts.com/aviasales/v3/prices_for_dates";


        public HomeController(ILogger<HomeController> logger)   
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SearchTickets(string origin, string destination, string departureTime) {
           
            HttpClient httpClient = new HttpClient();

            var requestQuery = BASE_URL + $"?origin={origin}&destination={destination}&sorting=price&cy=rub&page=1&token={TOKEN}";

            HttpResponseMessage? requestResult = await httpClient.GetAsync(requestQuery);

            return View("SearchRequest", JsonConvert.DeserializeObject<SearchData>(await requestResult.Content.ReadAsStringAsync()));
        }


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}