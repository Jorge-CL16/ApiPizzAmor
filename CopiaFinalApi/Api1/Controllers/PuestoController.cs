using Api1.Models.Response;
using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api1.Models.Request;

namespace Api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuestoController : ControllerBase
    {
        //Obtener el JSON de la tabla Puestos
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var lst = db.Puestos.ToList();
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

        //Agregar Puetos en formato JSON
        [HttpPost]
        public IActionResult Add(PuestoRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Puesto oCliente = new Puesto();
                    oCliente.Nombre = oModel.Puesto;
                    db.Puestos.Add(oCliente);
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

        //Editar Puesto en formato JSON
        [HttpPut]
        public IActionResult Edit(PuestoRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                   
                    Puesto oPuesto = db.Puestos.Find(oModel.IdPuesto);

                    if (oPuesto == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Puesto no encontrado";
                        return NotFound(oRespuesta);
                    }

                    oPuesto.Nombre = oModel.Puesto;
                    db.Entry(oPuesto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Puesto actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        //Eliminar Puesto
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var puesto = db.Puestos.Find(id);

                    if (puesto == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Puesto no encontrado";
                        return NotFound(oRespuesta);
                    }

                    db.Puestos.Remove(puesto);
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Puesto eliminado correctamente";
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
