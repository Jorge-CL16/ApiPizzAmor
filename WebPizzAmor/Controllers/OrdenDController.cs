using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebPizzAmor.Models;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebPizzAmor.Controllers
{
    public class OrdenDController : Controller
    {
        private readonly AppDbContext _context;

        public OrdenDController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OrdenD()
        {
            var idCliente = HttpContext.Session.GetInt32("IdCliente");

            if (idCliente == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Refrescos = await _context.Refrescos.ToListAsync();
            ViewBag.Repartidores = await _context.Repartidors.ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcesarOrden(CajaViewModel model)
        {
            var idCliente = HttpContext.Session.GetInt32("IdCliente");
            if (idCliente == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cliente = await _context.Clientes.FindAsync(idCliente);
            if (cliente == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Refrescos = await _context.Refrescos.ToListAsync();
                ViewBag.Repartidores = await _context.Repartidors.ToListAsync();
                return View("OrdenD", model);
            }

            try
            {
                var refresco = await _context.Refrescos.FindAsync(model.IdRefresco);
                if (refresco == null)
                {
                    ModelState.AddModelError("IdRefresco", "Refresco no encontrado.");
                    ViewBag.Refrescos = await _context.Refrescos.ToListAsync();
                    ViewBag.Repartidores = await _context.Repartidors.ToListAsync();
                    return View("OrdenD", model);
                }

                decimal precioBasePizza = model.TamanioPizza switch
                {
                    "Pequeña" => 50.00m,
                    "Mediana" => 80.00m,
                    "Grande" => 150.00m,
                    "Familiar" => 350.00m,
                    _ => 0.00m
                };

                decimal montoTotal = precioBasePizza + refresco.PrecioR;

                if (model.IngredientesSeleccionados != null && model.IngredientesSeleccionados.Any())
                {
                    montoTotal += model.IngredientesSeleccionados.Count * 10.00m;
                }

                var ordenDigital = new OrdenDigital
                {
                    IdCliente = idCliente.Value,
                    IdRepartidor = model.IdRepartidor,
                    IdRefresco = model.IdRefresco,
                    SaborIngredientes = model.SaborIngredientes,
                    TamanioPizza = model.TamanioPizza,
                    MontoTotal = montoTotal,
                    FechaD = DateTime.Now,
                    Correo = cliente.Correo,
                    IdSucursal = 1,
                    UrlOrdenD = "Null"
                };

                _context.OrdenDigitals.Add(ordenDigital);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "¡Orden Generada exitosamente!";
                return RedirectToAction("OrdenD");
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("", "Error con su orden, porfavor procesar la orden de nuevo.");
                ViewBag.Refrescos = await _context.Refrescos.ToListAsync();
                ViewBag.Repartidores = await _context.Repartidors.ToListAsync();
                return View("OrdenD", model);
            }
        }
    }
}