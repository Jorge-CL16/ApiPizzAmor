using Api1.Models.Request;
using Api1.Models.Response;
using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredienteExtraController : ControllerBase
    {
        // Obtener lista de ingredientes extra (JSON de la tabla IngredientesExtra)
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var lst = db.IngredientesExtras.ToList();
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

        // Agregar un nuevo ingrediente extra
        [HttpPost]
        public IActionResult Add(IngredienteExtraRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    IngredientesExtra oIngredienteExtra = new IngredientesExtra
                    {
                        IdOrden = oModel.IdOrden,
                        IdIngrediente = oModel.IdIngrediente
                    };
                    db.IngredientesExtras.Add(oIngredienteExtra);
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

        // Editar un ingrediente extra existente
        [HttpPut]
        public IActionResult Edit(IngredienteExtraRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    IngredientesExtra oIngredienteExtra = db.IngredientesExtras.Find(oModel.IdExtra);

                    if (oIngredienteExtra == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Ingrediente extra no encontrado";
                        return NotFound(oRespuesta);
                    }

                    oIngredienteExtra.IdOrden = oModel.IdOrden;
                    oIngredienteExtra.IdIngrediente = oModel.IdIngrediente;

                    db.Entry(oIngredienteExtra).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Ingrediente extra actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        // Eliminar un ingrediente extra
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var ingredienteExtra = db.IngredientesExtras.Find(id);

                    if (ingredienteExtra == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Ingrediente extra no encontrado";
                        return NotFound(oRespuesta);
                    }

                    db.IngredientesExtras.Remove(ingredienteExtra);
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Ingrediente extra eliminado correctamente";
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
