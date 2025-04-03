using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace WebPizzAmor.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string correo, string telefono)
        {
            var cliente = _context.Clientes
                .FirstOrDefault(c => c.Correo == correo && c.Telefono == telefono);

            if (cliente != null)
            {
                HttpContext.Session.SetInt32("IdCliente", cliente.IdCliente);
                HttpContext.Session.SetString("Usuario", cliente.Correo);
                HttpContext.Session.SetString("CorreoCliente", cliente.Correo);
                HttpContext.Session.SetString("DomicilioCliente", cliente.Domicilio);
                HttpContext.Session.SetString("ImagenPerfil", cliente.UrlImagen ?? "/img/default-user.png");

                /*/
                HttpContext.Session.SetString("ImagenPerfil", cliente.UrlImagen ?? "/img/default-user.png");*/

                return RedirectToAction("Principal", "Principal");
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
        public IActionResult Register(string nombre, string apellido, string correo, string telefono, string domicilio, IFormFile imagen)
        {
            var cliente = new Cliente
            {
                NombreC = nombre,
                ApellidoC = apellido,
                Correo = correo,
                Telefono = telefono,
                Domicilio = domicilio
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

                cliente.UrlImagen = "/img/" + uniqueFileName;
            }

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}