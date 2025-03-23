using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using System.Threading.Tasks;
using WebApi.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenDigitalController : ControllerBase
    {
        private readonly IOrdenDigitalService _ordenDigitalService;

        public OrdenDigitalController(IOrdenDigitalService ordenDigitalService)
        {
            _ordenDigitalService = ordenDigitalService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodasLasOrdenes()
        {
            var ordenes = await _ordenDigitalService.ObtenerTodasLasOrdenesAsync();
            return Ok(ordenes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerOrdenPorId(int id)
        {
            var orden = await _ordenDigitalService.ObtenerOrdenPorIdAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            return Ok(orden);
        }

        [HttpPost]
        public async Task<IActionResult> CrearOrden([FromBody] OrdenDigital orden)
        {
            if (orden == null)
            {
                return BadRequest("La orden es inválida.");
            }

            await _ordenDigitalService.CrearOrdenAsync(orden);
            return CreatedAtAction(nameof(ObtenerOrdenPorId), new { id = orden.IdOrdenD }, orden);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarOrden(int id, [FromBody] OrdenDigital orden)
        {
            if (id != orden.IdOrdenD)
            {
                return BadRequest("El ID de la orden no coincide.");
            }

            var ordenExistente = await _ordenDigitalService.ObtenerOrdenPorIdAsync(id);
            if (ordenExistente == null)
            {
                return NotFound();
            }

            await _ordenDigitalService.ActualizarOrdenAsync(orden);
            return NoContent();
        }

         
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarOrden(int id)
        {
            var orden = await _ordenDigitalService.ObtenerOrdenPorIdAsync(id);
            if (orden == null)
            {
                return NotFound();
            }

            await _ordenDigitalService.EliminarOrdenAsync(id);
            return NoContent();
        }
    }
}
