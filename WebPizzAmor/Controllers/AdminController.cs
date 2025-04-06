using Microsoft.AspNetCore.Mvc;

namespace WebPizzAmor.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }
    }
}
