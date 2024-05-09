using Microsoft.AspNetCore.Mvc;
using Mirea_Avia.Database;
using Mirea_Avia.Models;
using Mirea_Avia.Models.Search;
using Newtonsoft.Json;
using System.Diagnostics;


namespace Mirea_Avia.Controllers
{
    /**
     * <summary>Контроллер домашней страницы</summary>
     * **/
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public static Country[] countries;
        public static City[] cities;


        /**
         * <summary>Токен авторизации для доступа к API</summary>
         */
        public static readonly string TOKEN = "d776e6b299669e2e28eeac8d826333c0";

        /**
         * <summary>Базовый URL API</summary>
         */

        public static readonly string BASE_URL = "https://api.travelpayouts.com/aviasales/v3/prices_for_dates";


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            using (ApplicationContext db = new ApplicationContext())
            {
                countries = db.Countries.Select(x => x).ToArray();
                cities = db.Cities.Select(x => x).ToArray();
            }
        }

        /**
         * <summary>Запрос главной страницы</summary>
         * <returns>Представление главной страницы</returns>
         */
        public IActionResult Index()
        {
            return View();
        }

        /**
         * <summary>Обработчик формы поиска</summary>
         * <param name="origin">Город отправления</param>
         * <param name="destination">Город прибытия</param>
         * <param name="departureTime">Время отправления</param>
         * <returns>Представление страницы с найденными авиабилетами</returns>
         */
        [HttpPost]
        public async Task<IActionResult> SearchTickets(string origin, string destination, string departureTime)
        {

            HttpClient httpClient = new HttpClient();

            origin = cities.FirstOrDefault(x => x.city_name == origin).city_code;
            destination = cities.FirstOrDefault(x => x.city_name == destination).city_code;

            var requestQuery = BASE_URL + $"?origin={origin}&destination={destination}&sorting=price&cy=rub&page=1&token={TOKEN}";

            HttpResponseMessage? requestResult = await httpClient.GetAsync(requestQuery);

            return View("SearchRequest", JsonConvert.DeserializeObject<SearchResultModel>(await requestResult.Content.ReadAsStringAsync()));
        }

        /**
         * <summary>Запрос страницы политики конфиденциальности</summary>
         * <returns>Представление страницы политики конфиденциальности</returns>
         */
        public IActionResult Privacy()
        {
            return View();
        }

        //public IActionResult SignUp()
        //{
        //    return RedirectToAction("SignUp", "Account");
        //}

        //public IActionResult SignIn()
        //{
        //    return RedirectToAction("SignIn", "Account");
        //}

        /**
         * <summary>Запрос страницы отчета об ошибке (для разработчиков)</summary>
         * <returns>Представление страницы с отчетом об ошибке</returns>
         */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}