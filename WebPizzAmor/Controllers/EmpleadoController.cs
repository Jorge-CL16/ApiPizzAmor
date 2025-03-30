using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebPizzAmor.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly AppDbContext _context;

        public EmpleadoController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Empleado()
        {
            return View();
        }

        public IActionResult TablaEmpleado()
        {
            using (AppDbContext context = new AppDbContext())
            {
                return View(context.Empleados.ToList());
            }
        }

        [HttpPost]
        public IActionResult AgregarEmpleado(string nombre, string apellido, int edad, string sexo, string puesto)
        {
            var empleado = new Empleado
            {
                NombreE = nombre,
                ApellidoE = apellido,
                Edad = edad,
                Sexo = sexo,
                Puesto = puesto
            };

            _context.Empleados.Add(empleado);
            _context.SaveChanges();

            return RedirectToAction("TablaEmpleado");
        }

        public IActionResult ModificarEmpleado(int idEmpleado)
        {
            var empleado = _context.Empleados.Find(idEmpleado);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        [HttpPost]
        public IActionResult ModificarEmpleado(int idEmpleado, string nombre, string apellido, int edad, string sexo, string puesto)
        {
            var empleado = _context.Empleados.Find(idEmpleado);
            if (empleado == null)
            {
                return NotFound();
            }

            empleado.NombreE = nombre;
            empleado.ApellidoE = apellido;
            empleado.Edad = edad;
            empleado.Sexo = sexo;
            empleado.Puesto = puesto;


            _context.Entry(empleado).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("TablaEmpleado");
        }
    }
}
