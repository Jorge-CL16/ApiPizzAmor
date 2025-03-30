using Microsoft.AspNetCore.Mvc;
using WebPizzAmor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebPizzAmor.Controllers
{
    public class OrdenFisicaController : Controller
    {
        private readonly AppDbContext _context;

        public OrdenFisicaController(AppDbContext context)
        {
            _context = context;
        }

        // Lo demas y/OrdenFisica/Crear
        public async Task<IActionResult> Crear()
        {
            var viewModel = new OrdenFisicaViewModel
            {
                Empleados = await _context.Empleados
                    .Select(e => new SelectListItem
                    {
                        Value = e.IdEmpleado.ToString(),
                        Text = e.NombreE
                    }).ToListAsync(),

                Refrescos = await _context.Refrescos
                    .Select(r => new SelectListItem
                    {
                        Value = r.IdRefresco.ToString(),
                        Text = $"{r.Marca} (${r.PrecioR})"
                    }).ToListAsync(),

                TamanioPizza = "Mediana",
                MontoTotal = 75 
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(OrdenFisicaViewModel orden)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    orden.MontoTotal = CalcularTotal(orden);

                    var nuevaOrden = new OrdenFisica
                    {
                        IdEmpleado = orden.IdEmpleado,
                        IdRefresco = orden.IdRefresco,
                        SaborIngredientes = orden.SaborIngredientes,
                        TamanioPizza = orden.TamanioPizza,
                        MontoTotal = orden.MontoTotal,
                        FechaF = DateTime.Now
                    };

                    _context.OrdenFisicas.Add(nuevaOrden);
                    var result = await _context.SaveChangesAsync();
                    if (result == 0)
                    {
                        ModelState.AddModelError("", "Error al guardar en la base de datos.");
                    }

                    TempData["SuccessMessage"] = "Orden guardada correctamente!";
                    return RedirectToAction("Crear");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar: {ex.Message}");
                ModelState.AddModelError("", "Error al guardar la orden. Intente nuevamente.");
            }

            orden.Empleados = await _context.Empleados
                .Select(e => new SelectListItem { Value = e.IdEmpleado.ToString(), Text = e.NombreE })
                .ToListAsync();

            orden.Refrescos = await _context.Refrescos
                .Select(r => new SelectListItem { Value = r.IdRefresco.ToString(), Text = $"{r.Marca} (${r.PrecioR})" })
                .ToListAsync();

            return View(orden);
        }

        private decimal CalcularTotal(OrdenFisicaViewModel orden)
        {
            decimal total = 0;

            switch (orden.TamanioPizza)
            {
                case "Pequeña": total += 50; break;
                case "Mediana": total += 75; break;
                case "Grande": total += 100; break;
            }

            switch (orden.SaborIngredientes)
            {
                case "Hawaiana": total += 10; break;
                case "Peperoni": total += 15; break;
            }

            if (orden.IdRefresco > 0)
            {
                var refresco = _context.Refrescos.Find(orden.IdRefresco);
                if (refresco != null) total += refresco.PrecioR;
            }

            return total;
        }
    }
}