using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebPizzAmor.Controllers
{
    public class OrdenController : Controller
    {

        private readonly AppDbContext _context;

        public OrdenController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Orden()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarOrden(int empleado, int refresco, string ingredientes, string tamaño, DateTime fecha, string url, int monto)
        {
            var orden = new OrdenFisica
            {
                IdEmpleado = empleado,
                IdRefresco = refresco,
                SaborIngredientes = ingredientes,
                TamanioPizza = tamaño,
                FechaF = fecha,
                UrlOrdenF = url,
                MontoTotal = monto
            };

            _context.OrdenFisicas.Add(orden);
            _context.SaveChanges();

            return RedirectToAction("Orden");
        }
    }
}
