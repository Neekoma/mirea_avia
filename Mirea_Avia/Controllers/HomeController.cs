﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mirea_Avia.Database;
using Mirea_Avia.Models;
using Mirea_Avia.Models.Search;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Mirea_Avia.Models.Users;

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


        ///
        /// <summary>Токен авторизации для доступа к API</summary>
        ///
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
        public async Task<IActionResult> SearchTickets(string origin, string destination, int? transfers, DateTime? departureTime)
        {
            // departure_at=2024-05-17

            HttpClient httpClient = new HttpClient();

            origin = cities.FirstOrDefault(x => x.city_name == origin).city_code;
            destination = cities.FirstOrDefault(x => x.city_name == destination).city_code;

            string requestQuery = string.Empty;

            if (departureTime == null)
            {
                requestQuery = BASE_URL + $"?origin={origin}&destination={destination}&sorting=price&cy=rub&page=1&token={TOKEN}";
            }
            else
            {
                requestQuery = BASE_URL + $"?origin={origin}&destination={destination}&sorting=price&cy=rub&page=1&token={TOKEN}&departure_at={departureTime?.Year}-{(departureTime?.Month < 10 ? ("0" + departureTime?.Month) : departureTime?.Month)}-{(departureTime?.Day < 10 ? ("0" + departureTime?.Day) : departureTime?.Day)}";
            }


            HttpResponseMessage? requestResult = await httpClient.GetAsync(requestQuery);

            SearchResultModel model = JsonConvert.DeserializeObject<SearchResultModel>(await requestResult.Content.ReadAsStringAsync());

            if (transfers != null)
            {
                transfers = transfers < 0 ? 0 : transfers;
                model.data = model.data.Where(x => x.transfers == transfers).ToList();
            }

            foreach (var fly in model.data)
            {
                var duration = float.Parse(fly.duration) / 60;
                var departure = DateTime.Parse(fly.timeOfDepartue);
                var fly_hours = (int)(duration);
                var fly_minutes = Math.Round(60 * (duration % 1));
                fly.arrivalAt = departure.AddHours(fly_hours).AddMinutes(fly_minutes).ToString("dd.MM.yyyy HH:mm");
                fly.duration = $"{fly_hours} ч. {fly_minutes} м.";

                fly.airline = $"https://pics.avs.io/200/200/{fly.airline}.png";
            }

            return View("SearchRequest", model);
        }
        /// <input hidden type="text" value=@Model.cityOfDepartue name="city_from" />
        ///   <input hidden type="text" value=@Model.cityOfArrival name = "city_to" />
        // < input hidden type = "number" value=@Model.price name = "price" />
        // < input hidden type = "datetime" value=@Model.timeOfDepartue name = "date_from" />
        [HttpPost]
        public async Task<IActionResult> BuyTicket(string city_from, string city_to, double price, DateTime date_from)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var user = await db.Users.FirstOrDefaultAsync(x => x.Username == User.Identity.Name);
                    var cityDeparture = await db.Cities.FirstOrDefaultAsync(x => x.city_code == city_from);
                    var cityArrival = await db.Cities.FirstOrDefaultAsync(x => x.city_code == city_to);
                    await db.PurchasedTickets.AddAsync(new PurchasedTicket { FK_Ticket_User = user, FK_Ticket_City_From = cityDeparture, FK_Ticket_City_To = cityArrival, date_from = date_from, price = price });
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        /**
         * <summary>Запрос страницы политики конфиденциальности</summary>
         * <returns>Представление страницы политики конфиденциальности</returns>
         */
        public IActionResult Privacy()
        {
            return View();
        }

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