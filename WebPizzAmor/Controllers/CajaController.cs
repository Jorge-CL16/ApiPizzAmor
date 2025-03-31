using Microsoft.AspNetCore.Mvc;
using WebPizzAmor.Models;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WebPizzAmor.Controllers
{
    public class CajaController : Controller
    {
        private readonly AppDbContext _context;

        public CajaController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Caja()
        {
            var empleados = await _context.Empleados.ToListAsync();
            var refrescos = await _context.Refrescos.ToListAsync();

            ViewBag.Empleados = empleados;
            ViewBag.Refrescos = refrescos;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcesarOrden(CajaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var refresco = await _context.Refrescos.FindAsync(model.IdRefresco);
                var empleado = await _context.Empleados.FindAsync(model.IdEmpleado);

                if (refresco != null && empleado != null)
                {
                    decimal precioBasePizza = model.TamanioPizza switch
                    {
                        "Pequeña" => 50.00m,
                        "Mediana" => 75.00m,
                        "Grande" => 100.00m,
                        "Familiar" => 250.00m,
                        _ => 0.00m
                    };

                    decimal montoTotal = precioBasePizza + refresco.PrecioR;

                    if (model.IngredientesSeleccionados != null && model.IngredientesSeleccionados.Any())
                    {
                        montoTotal += model.IngredientesSeleccionados.Count * 5.00m; 
                    }

                    var ordenFisica = new OrdenFisica
                    {
                        IdEmpleado = model.IdEmpleado,
                        IdRefresco = model.IdRefresco,
                        SaborIngredientes = model.SaborIngredientes,
                        TamanioPizza = model.TamanioPizza,
                        MontoTotal = montoTotal,
                        FechaF = DateTime.Now,
                        UrlOrdenF = "Null"
                    };

                    _context.OrdenFisicas.Add(ordenFisica);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Caja"); 
                }

                ModelState.AddModelError("", "Empleado o Refresco no encontrado.");
            }

            return View("Caja", model);
        }
    }
}
