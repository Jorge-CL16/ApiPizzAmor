using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using WebPizzAmor.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace WebPizzAmor.Controllers
{
    public class MenuController : Controller
    {
        private readonly HttpClient _httpClient;

        public MenuController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Menu()
        {
            var url = "https://localhost:44305/api/Menu";
            var response = await _httpClient.GetStringAsync(url);

            if (string.IsNullOrWhiteSpace(response))
            {
                return View(new List<Pizza>());
            }

            var jsonObject = JsonConvert.DeserializeObject<JObject>(response);
            var valuesArray = jsonObject?["$values"]?.ToObject<List<PizzaData>>();

            if (valuesArray == null || !valuesArray.Any())
            {
                return View(new List<Pizza>());
            }


            var pizzaData = valuesArray
            .Where(pizza => pizza != null) 
            .Select(pizza => new Pizza
            {
                NombrePizza = pizza.NombrePizza ?? "Sin nombre",
                DescripPizza = pizza.DescripPizza ?? "Sin descripción",
                UrlMenu = !string.IsNullOrEmpty(pizza.UrlMenu) ? pizza.UrlMenu : "https://via.placeholder.com/150", 
                Precio = pizza.Precio,
                TamanioPizza = pizza.TamanioPizza ?? "Desconocido"
            }).ToList();


            return View(pizzaData);
        }
    }

    public class PizzaData
    {
        public string NombrePizza { get; set; }
        public string DescripPizza { get; set; }
        public string UrlMenu { get; set; }
        public double Precio { get; set; }
        public string TamanioPizza { get; set; }
    }
}
