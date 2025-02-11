using Api1.Models.Response;
using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api1.Models.Request;

namespace Api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallesOrdenController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var lst = db.DetallesOrdens.Include(d => d.IdMenuNavigation).Include(d => d.IdOrdenNavigation).ToList();
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
        public IActionResult Add(DetallesOrdenRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    DetallesOrden oDetalle = new DetallesOrden
                    {
                        IdOrden = oModel.IdOrden,
                        IdMenu = oModel.IdMenu,
                        Cantidad = oModel.Cantidad,
                        PrecioUnitario = oModel.PrecioUnitario
                    };
                    db.DetallesOrdens.Add(oDetalle);
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


        // Editar un detalle de orden existente
        [HttpPut]
        public IActionResult Edit(DetallesOrdenRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    DetallesOrden oDetalle = db.DetallesOrdens.Find(oModel.IdDetalle);

                    if (oDetalle == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Detalle de orden no encontrado";
                        return NotFound(oRespuesta);
                    }

                    oDetalle.IdOrden = oModel.IdOrden;
                    oDetalle.IdMenu = oModel.IdMenu;
                    oDetalle.Cantidad = oModel.Cantidad;
                    oDetalle.PrecioUnitario = oModel.PrecioUnitario;

                    db.Entry(oDetalle).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Detalle de orden actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        // Eliminar un detalle de orden
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var detalle = db.DetallesOrdens.Find(id);

                    if (detalle == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Detalle de orden no encontrado";
                        return NotFound(oRespuesta);
                    }

                    db.DetallesOrdens.Remove(detalle);
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Detalle de orden eliminado correctamente";
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
