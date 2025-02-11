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
    public class IngredientesController : ControllerBase
    {
        // Obtener todos los ingredientes
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var lst = db.Ingredientes.ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.InnerException?.Message ?? ex.Message;
            }
            return Ok(oRespuesta);
        }

        // Agregar un nuevo ingrediente
        [HttpPost]
        public IActionResult Add(IngredienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Ingrediente oIngrediente = new Ingrediente
                    {
                        Nombre = oModel.Nombre
                    };
                    db.Ingredientes.Add(oIngrediente);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Ingrediente agregado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.InnerException?.Message ?? ex.Message;
            }
            return Ok(oRespuesta);
        }

        // Editar un ingrediente
        [HttpPut]
        public IActionResult Edit(IngredienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Ingrediente oIngrediente = db.Ingredientes.Find(oModel.IdIngrediente);

                    if (oIngrediente == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Ingrediente no encontrado";
                        return NotFound(oRespuesta);
                    }

                    oIngrediente.Nombre = oModel.Nombre;
                    db.Entry(oIngrediente).State = EntityState.Modified;
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Ingrediente actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.InnerException?.Message ?? ex.Message;
            }
            return Ok(oRespuesta);
        }

        // Eliminar un ingrediente
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var ingrediente = db.Ingredientes.Find(id);

                    if (ingrediente == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Ingrediente no encontrado";
                        return NotFound(oRespuesta);
                    }

                    db.Ingredientes.Remove(ingrediente);
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Ingrediente eliminado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.InnerException?.Message ?? ex.Message;
            }
            return Ok(oRespuesta);
        }
    }
}
