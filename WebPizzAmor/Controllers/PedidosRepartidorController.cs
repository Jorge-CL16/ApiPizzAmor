using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using WebPizzAmor.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace WebPizzAmor.Controllers
{
    public class PedidosRepartidorController : Controller
    {
        private readonly HttpClient _httpClient;

        public PedidosRepartidorController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> PedidosRepartidor()
        {
            var repartidorId = HttpContext.Session.GetInt32("RepartidorId");
            if (repartidorId == null)
            {
                return RedirectToAction("Repartidor", "Repartidor");
            }

            var url = "https://localhost:44305/api/OrdenDigital";
            var response = await _httpClient.GetStringAsync(url);
            var pedidos = JsonConvert.DeserializeObject<List<OrdenDigital>>(response);

            var pedidosData = pedidos
                .Where(p => p.IdRepartidor == repartidorId)
                .Select(p => new OrdenDigitalData
                {
                    IdOrdenD = p.IdOrdenD,
                    NombreCliente = p.IdClienteNavigation != null
                                    ? p.IdClienteNavigation.NombreC + " " + p.IdClienteNavigation.ApellidoC
                                    : "Desconocido",
                    Telefono = p.IdClienteNavigation?.Telefono ?? "No disponible",
                    Domicilio = p.IdClienteNavigation?.Domicilio ?? "No disponible",
                    SaborIngredientes = p.SaborIngredientes,
                    TamanioPizza = p.TamanioPizza,
                    MontoTotal = p.MontoTotal,
                }).ToList();

            return View(pedidosData);
        }


    }
    /*
    public class ClienteData
    {
        public string NombreC { get; set; }
        public string ApellidoC { get; set; }
        public string Telefono { get; set; }
        public string Domicilio { get; set; }
    }*/
}
