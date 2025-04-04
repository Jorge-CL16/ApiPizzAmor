using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebPizzAmor.Controllers
{
    public class RefrescoController : Controller
    {
        private readonly AppDbContext _context;

        public RefrescoController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Refresco()
        {
            return View();
        }
        public IActionResult TablaRefresco()
        {
            using (AppDbContext context = new AppDbContext())
            {
                return View(context.Refrescos.ToList());
            }
        }

        [HttpPost]
        public IActionResult AgregarRefresco(string marca, decimal precio, string tamanio)
        {
            var refresco = new DataAccess.Models.Refresco
            {
                Marca = marca,
                PrecioR = precio,
                TamanioR = tamanio
            };

            _context.Refrescos.Add(refresco);
            _context.SaveChanges();

            return RedirectToAction("TablaRefresco");
        }

        public IActionResult ModificarRefresco(int idRefresco)
        {
            var refresco = _context.Refrescos.Find(idRefresco);
            if (refresco == null)
            {
                return NotFound();
            }
            return View(refresco);
        }

        [HttpPost]
        public IActionResult ModificarRefresco(int idRefresco, string marca, decimal precio, string tamanio)
        {
            var refresco = _context.Refrescos.Find(idRefresco);
            if (refresco == null)
            {
                return NotFound();
            }

            refresco.Marca = marca;
            refresco.PrecioR = precio;
            refresco.TamanioR = tamanio;


            _context.Entry(refresco).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("TablaRefresco");
        }
    }
}