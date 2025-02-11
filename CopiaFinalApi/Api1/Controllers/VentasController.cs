using Api1.Models.Request;
using Api1.Models.Response;
using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        // Obtener todas las ventas
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var lst = db.Ventas
                        .Include(v => v.IdOrdenNavigation) 
                        .ToList();

                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        [HttpPost]
        public IActionResult Add(VentaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Venta oVenta = new Venta();
                    oVenta.IdOrden = oModel.IdOrden;
                    oVenta.FechaVenta = oModel.FechaVenta;
                    oVenta.MontoTotal = oModel.MontoTotal;

                    db.Ventas.Add(oVenta);
                    db.SaveChanges(); 

                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = $"Error: {ex.Message}";

               
                if (ex.InnerException != null)
                {
                    oRespuesta.Mensaje += $" - Inner Exception: {ex.InnerException.Message}";
                }
            }

            return Ok(oRespuesta);
        }


        // Editar una venta existente
        [HttpPut]
        public IActionResult Edit(VentaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Venta oVenta = db.Ventas.Find(oModel.IdVenta);

                    if (oVenta == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Venta no encontrada";
                        return NotFound(oRespuesta);
                    }

                    oVenta.IdOrden = oModel.IdOrden;
                    oVenta.FechaVenta = oModel.FechaVenta;
                    oVenta.MontoTotal = oModel.MontoTotal;

                    db.Entry(oVenta).State = EntityState.Modified;
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Venta actualizada correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        // Eliminar una venta
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var venta = db.Ventas.Find(id);

                    if (venta == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Venta no encontrada";
                        return NotFound(oRespuesta);
                    }

                    db.Ventas.Remove(venta);
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Venta eliminada correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }
    }
}
