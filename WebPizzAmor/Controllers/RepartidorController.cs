using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DataAccess.Models;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebPizzAmor.Models;

namespace WebPizzAmor.Controllers
{
    public class RepartidorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public RepartidorController(AppDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        [HttpGet]
        public IActionResult Repartidor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(int id, string placa)
        {
            var repartidor = _context.Repartidors
                .FirstOrDefault(r => r.IdRepartidor == id && r.Placa == placa);

            if (repartidor != null)
            {
                HttpContext.Session.SetInt32("RepartidorId", repartidor.IdRepartidor);
                HttpContext.Session.SetString("RepartidorNombre", repartidor.NombreR);
                HttpContext.Session.SetString("ImagenPerfil", repartidor.UrlRepartidor ?? "/img/default-user.png");

                return RedirectToAction("PedidosRepartidor", "PedidosRepartidor");
            }

            ViewBag.ErrorMessage = "Credenciales incorrectas. Inténtalo de nuevo.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string nombre, string apellido, string placa, IFormFile imagen)
        {
            var repartidor = new Repartidor
            {
                NombreR = nombre,
                ApellidoR = apellido,
                Placa = placa
            };

            if (imagen != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imagen.CopyTo(stream);
                }

                repartidor.UrlRepartidor = "/img/" + uniqueFileName;
            }

            _context.Repartidors.Add(repartidor);
            _context.SaveChanges();

            return RedirectToAction("Login");
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
                    MontoTotal = p.MontoTotal
                }).ToList();

            return View(pedidosData);
        }

        public IActionResult Ordenes()
        {
            int? repartidorId = HttpContext.Session.GetInt32("RepartidorId");
            if (repartidorId == null)
                return RedirectToAction("PedidosRepartidor");

            var pedidos = _context.OrdenDigitals
                .Where(o => o.IdRepartidor == repartidorId)
                .Select(o => new OrdenDigitalData
                {
                    IdOrdenD = o.IdOrdenD,
                    NombreCliente = o.IdClienteNavigation != null
                                    ? o.IdClienteNavigation.NombreC + " " + o.IdClienteNavigation.ApellidoC
                                    : "Desconocido",
                    Telefono = o.IdClienteNavigation != null ? o.IdClienteNavigation.Telefono : "No disponible",
                    Domicilio = o.IdClienteNavigation != null ? o.IdClienteNavigation.Domicilio : "No disponible",
                    SaborIngredientes = o.SaborIngredientes,
                    TamanioPizza = o.TamanioPizza,
                    MontoTotal = o.MontoTotal
                }).ToList();

            return View(pedidos);
        }
    }
}
