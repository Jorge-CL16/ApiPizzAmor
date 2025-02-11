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
    public class OrdenesController : ControllerBase
    {
        // Obtener lista de órdenes (JSON de la tabla Ordenes)
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var lst = db.Ordenes
                        .Include(o => o.IdClienteNavigation)
                        .Include(o => o.IdEmpleadoNavigation)
                        .Include(o => o.DetallesOrdens)
                        .Include(o => o.IngredientesExtras)
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

        // Agregar una nueva orden
        [HttpPost]
        public IActionResult Add(OrdeneRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Ordene oOrdene = new Ordene
                    {
                        IdCliente = oModel.IdCliente,
                        IdEmpleado = oModel.IdEmpleado,
                        FechaOrden = oModel.FechaOrden,
                        MontoTotal = oModel.MontoTotal,
                        CreadoEn = oModel.CreadoEn,
                        ActualizadoEn = oModel.ActualizadoEn
                    };

                    db.Ordenes.Add(oOrdene);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        // Editar una orden existente
        [HttpPut]
        public IActionResult Edit(OrdeneRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Ordene oOrdene = db.Ordenes.Find(oModel.IdOrden);

                    if (oOrdene == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Orden no encontrada";
                        return NotFound(oRespuesta);
                    }

                    oOrdene.IdCliente = oModel.IdCliente;
                    oOrdene.IdEmpleado = oModel.IdEmpleado;
                    oOrdene.FechaOrden = oModel.FechaOrden;
                    oOrdene.MontoTotal = oModel.MontoTotal;
                    oOrdene.CreadoEn = oModel.CreadoEn;
                    oOrdene.ActualizadoEn = oModel.ActualizadoEn;

                    db.Entry(oOrdene).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Orden actualizada correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        // Eliminar una orden
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var ordene = db.Ordenes.Find(id);

                    if (ordene == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Orden no encontrada";
                        return NotFound(oRespuesta);
                    }

                    db.Ordenes.Remove(ordene);
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Orden eliminada correctamente";
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
