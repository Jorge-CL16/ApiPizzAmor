using Api1.Models.Response;
using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api1.Models.Request;

namespace Api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TamañoDePizzaController : ControllerBase
    {
        //Obtener tamaño de las pizzas
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var lst = db.TamanoDePizzas.ToList();
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


        //Agregar el JSON del tamaño de Pizza
        [HttpPost]
        public IActionResult Add(TamañoDePizzaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {

                    TamanoDePizza oTamañoDePizza = new TamanoDePizza();
                    oTamañoDePizza.Nombre = oModel.Nombre;
                    oTamañoDePizza.Precio = oModel.Precio;
                    db.TamanoDePizzas.Add(oTamañoDePizza);
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

        //Editar Tamaño De Pizza
        [HttpPut]
        public IActionResult Edit(TamañoDePizzaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    TamanoDePizza oEmpleado = db.TamanoDePizzas.Find(oModel.IdTamano);

                    if (oEmpleado == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Tamaño no encontrado";
                        return NotFound(oRespuesta);
                    }

                    oEmpleado.Nombre = oModel.Nombre;
                    oEmpleado.Precio = oModel.Precio;
              

                    db.Entry(oEmpleado).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Tamaño actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        //Eliminar Tamaño Pizza
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var tamaño = db.TamanoDePizzas.Find(id);

                    if (tamaño == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Tamaño no encontrado";
                        return NotFound(oRespuesta);
                    }

                    db.TamanoDePizzas.Remove(tamaño);
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Tamaño eliminado correctamente";
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
