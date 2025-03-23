using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using WebApi.Interfaces;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosMenus()
        {
            var menus = await _menuService.ObtenerTodosLosMenusAsync();
            return Ok(menus);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerMenuPorId(int id)
        {
            var menu = await _menuService.ObtenerMenuPorIdAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }

        [HttpPost]
        public async Task<IActionResult> CrearMenu([FromBody] Menu menu)
        {
            if (menu == null)
            {
                return BadRequest("El menú es inválido.");
            }

            await _menuService.CrearMenuAsync(menu);
            return CreatedAtAction(nameof(ObtenerMenuPorId), new { id = menu.IdMenu }, menu);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarMenu(int id, [FromBody] Menu menu)
        {
            if (id != menu.IdMenu)
            {
                return BadRequest("El ID del menú no coincide.");
            }

            var menuExistente = await _menuService.ObtenerMenuPorIdAsync(id);
            if (menuExistente == null)
            {
                return NotFound();
            }

            await _menuService.ActualizarMenuAsync(menu);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMenu(int id)
        {
            var menu = await _menuService.ObtenerMenuPorIdAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            await _menuService.EliminarMenuAsync(id);
            return NoContent();
        }
    }
}
