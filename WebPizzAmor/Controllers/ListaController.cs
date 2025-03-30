using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using WebPizzAmor.Models;

namespace WebPizzAmor.Controllers
{
    public class ListaController : Controller
    {
        private readonly AppDbContext _context;

        public ListaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Lista()
        {
            using (AppDbContext context = new AppDbContext())
            {
                return View(context.OrdenFisicas.ToList());
            }
        }
        //public IActionResult TablaOrdenF()
        //{
        //    using (AppDbContext context = new AppDbContext())
        //    {
        //        return View(context.OrdenFisicas.ToList());
        //    }
        //}
    }
}
