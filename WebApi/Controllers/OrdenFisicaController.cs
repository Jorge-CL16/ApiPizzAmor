using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using System.Threading.Tasks;
using WebApi.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenFisicaController : ControllerBase
    {
        private readonly IOrdenFisicaService _ordenFisicaService;

        public OrdenFisicaController(IOrdenFisicaService ordenFisicaService)
        {
            _ordenFisicaService = ordenFisicaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodasLasOrdenes()
        {
            var ordenes = await _ordenFisicaService.ObtenerTodasLasOrdenesAsync();
            return Ok(ordenes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerOrdenPorId(int id)
        {
            var orden = await _ordenFisicaService.ObtenerOrdenPorIdAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            return Ok(orden);
        }

        [HttpPost]
        public async Task<IActionResult> CrearOrden([FromBody] OrdenFisica orden)
        {
            if (orden == null)
            {
                return BadRequest("La orden es inválida.");
            }

            await _ordenFisicaService.CrearOrdenAsync(orden);
            return CreatedAtAction(nameof(ObtenerOrdenPorId), new { id = orden.IdOrdenF }, orden);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarOrden(int id, [FromBody] OrdenFisica orden)
        {
            if (id != orden.IdOrdenF)
            {
                return BadRequest("El ID de la orden no coincide.");
            }

            var ordenExistente = await _ordenFisicaService.ObtenerOrdenPorIdAsync(id);
            if (ordenExistente == null)
            {
                return NotFound();
            }

            await _ordenFisicaService.ActualizarOrdenAsync(orden);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarOrden(int id)
        {
            var orden = await _ordenFisicaService.ObtenerOrdenPorIdAsync(id);
            if (orden == null)
            {
                return NotFound();
            }

            await _ordenFisicaService.EliminarOrdenAsync(id);
            return NoContent();
        }
    }
}