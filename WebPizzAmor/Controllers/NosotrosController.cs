using Microsoft.AspNetCore.Mvc;

namespace WebPizzAmor.Controllers
{
    public class NosotrosController : Controller
    {
        public IActionResult Nosotros()
        {
            return View();
        }
    }
}
