using Api1.Models.Response;
using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api1.Models.Request;

namespace Api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposDePizzaController : ControllerBase
    {
        //Obtener ej JSON de la tabla Tipos De Pizza
        [HttpGet]

        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var lst = db.TiposDePizzas.ToList();
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

        //Agregar el JSON del tipo de Pizza
        [HttpPost]
        public IActionResult Add(TipoDePizzaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    TiposDePizza oTipoDePizza = new TiposDePizza
                    {
                        Nombre = oModel.Nombre
                    };

                    db.TiposDePizzas.Add(oTipoDePizza);
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Tipo de pizza agregado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = ex.InnerException?.Message ?? ex.Message; 
            }
            return Ok(oRespuesta);
        }



        //Editar en formato JSON
        [HttpPut]
        public IActionResult Edit(TipoDePizzaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    TiposDePizza oTiposDePizza = db.TiposDePizzas.Find(oModel.IdTipoPizza);

                    if (oTiposDePizza == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Tipos de Pizza no encontrado";
                        return NotFound(oRespuesta);
                    }

                    oTiposDePizza.Nombre = oModel.Nombre;

                    db.Entry(oTiposDePizza).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Tipo de pizza actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        //Eliminar Tipo De Pizza
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var tipoPizza = db.TiposDePizzas.Find(id);

                    if (tipoPizza == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Tipo Pizza no encontrado";
                        return NotFound(oRespuesta);
                    }

                    db.TiposDePizzas.Remove(tipoPizza);
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Tipo Pizza eliminado correctamente";
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
