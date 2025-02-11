using Api1.Models.Request;
using Api1.Models.Response;
using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefrescosController : ControllerBase
    {

        // Obtener todos los refrescos
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var lst = db.Refrescos.ToList();
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

        // Agregar un refresco
        [HttpPost]
        public IActionResult Add(RefrescoRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Refresco oRefresco = new Refresco
                    {
                        Nombre = oModel.Nombre,
                        Precio = oModel.Precio,
                        Tamano = oModel.Tamano
                    };

                    db.Refrescos.Add(oRefresco);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Refresco agregado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        // Editar un refresco
        [HttpPut]
        public IActionResult Edit(RefrescoRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Refresco oRefresco = db.Refrescos.Find(oModel.IdRefresco);

                    if (oRefresco == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Refresco no encontrado";
                        return NotFound(oRespuesta);
                    }

                    oRefresco.Nombre = oModel.Nombre;
                    oRefresco.Precio = oModel.Precio;
                    oRefresco.Tamano = oModel.Tamano;

                    db.Entry(oRefresco).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Refresco actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        // Eliminar un refresco
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var refresco = db.Refrescos.Find(id);

                    if (refresco == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Refresco no encontrado";
                        return NotFound(oRespuesta);
                    }

                    db.Refrescos.Remove(refresco);
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Refresco eliminado correctamente";
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
