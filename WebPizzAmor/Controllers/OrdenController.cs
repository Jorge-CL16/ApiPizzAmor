using Microsoft.AspNetCore.Mvc;
using WebPizzAmor.Models;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WebPizzAmor.Controllers
{
    public class OrdenController : Controller
    { 
        
            private readonly AppDbContext _context;

            public OrdenController(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Caja()
            {
                var cliente = await _context.Clientes.ToListAsync();
                var sucursal = await _context.Sucursals.ToListAsync();
                var refrescos = await _context.Refrescos.ToListAsync();

                ViewBag.Clientes = cliente;
                ViewBag.Refrescos = refrescos;
                ViewBag.Sucursals = sucursal;

                return View();
            }

            [HttpPost]
            public async Task<IActionResult> ProcesarOrden(OrdenDigitalViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var refresco = await _context.Refrescos.FindAsync(model.IdRefresco);
                    var cliente = await _context.Clientes.FindAsync(model.IdEmpleado);

                    if (refresco != null && cliente != null)
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

                        var OrdenDigital = new OrdenDigital
                        {
                            IdCliente = model.IdCliente,
                            IdRefresco = model.IdRefresco,
                            SaborIngredientes = model.SaborIngredientes,
                            TamanioPizza = model.TamanioPizza,
                            MontoTotal = montoTotal,
                            FechaF = DateTime.Now,
                            UrlOrdenF = "Null"
                        };

                        _context.OrdenDigitals.Add(OrdenDigital);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Caja");
                    }

                    ModelState.AddModelError("", "Empleado o Refresco no encontrado.");
                }

                return View("Caja", model);
            }
        
    }
}
    

