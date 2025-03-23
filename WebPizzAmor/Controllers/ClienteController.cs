using System.Net;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebPizzAmor.Controllers
{
    public class ClienteController : Controller
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Cliente()
        {
            return View();
        }
        public IActionResult TablaCliente()
        {
            using (AppDbContext context = new AppDbContext())
            {
                return View(context.Clientes.ToList());
            }
        }

        [HttpPost]
        public IActionResult AgregarCliente(string nombre, string apellido, string correo, IFormFile imagen, string telefono, string domicilio)
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

            return RedirectToAction("TablaCliente");
        }

        public IActionResult ModificarCliente(int idCliente)
        {
            var cliente = _context.Clientes.Find(idCliente);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public IActionResult ModificarCliente(int idCliente, string nombre, string apellido, string correo, IFormFile imagen, string telefono, string domicilio)
        {
            var cliente = _context.Clientes.Find(idCliente);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.NombreC = nombre;
            cliente.ApellidoC = apellido;
            cliente.Correo = correo;
            cliente.Telefono = telefono;
            cliente.Domicilio = domicilio;

            if (imagen != null)
            {
                var actualizarEnFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                if (!Directory.Exists(actualizarEnFolder))
                {
                    Directory.CreateDirectory(actualizarEnFolder);
                }

                var imgArch = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                var filePath = Path.Combine(actualizarEnFolder, imgArch);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imagen.CopyTo(stream);
                }

                cliente.UrlImagen = "/img/" + imgArch;
            }

            _context.Entry(cliente).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("TablaCliente");
        }




    }
}