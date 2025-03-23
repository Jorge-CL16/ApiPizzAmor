using Microsoft.AspNetCore.Mvc;

namespace WebPizzAmor.Controllers
{
    public class PrincipalController : Controller
    {
        public IActionResult Principal()
        {
            return View();
        }
    }
}
