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
    public class EmpleadoController : ControllerBase
    {
        //Obtener ej JSON de la tabla Empleado
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var lst = db.Empleados.ToList();
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

        //Agregar Empleado
        [HttpPost]
        public IActionResult Add(EmpleadoRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {

                    Empleado oEmpleado = new Empleado();
                    oEmpleado.Nombre = oModel.Nombre;
                    oEmpleado.Apellido = oModel.Apellido;
                    oEmpleado.Edad = oModel.Edad;
                    oEmpleado.Sexo = oModel.Sexo;
                    oEmpleado.FechaContratacion = DateOnly.FromDateTime(oModel.FechaContratacion);
                    oEmpleado.IdPuesto = oModel.IdPuesto;
                    db.Empleados.Add(oEmpleado);
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

        //Editar Empleado
        [HttpPut]
        public IActionResult Edit(EmpleadoRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var oEmpleado = db.Empleados.Find(oModel.IdEmpleado);
                    if (oEmpleado == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Empleado no encontrado";
                        return NotFound(oRespuesta);
                    }

                    oEmpleado.Nombre = oModel.Nombre;
                    oEmpleado.Apellido = oModel.Apellido;
                    oEmpleado.Edad = oModel.Edad;
                    oEmpleado.Sexo = oModel.Sexo;
                    oEmpleado.FechaContratacion = DateOnly.FromDateTime(oModel.FechaContratacion);
                    oEmpleado.IdPuesto = oModel.IdPuesto;

                    db.Entry(oEmpleado).State = EntityState.Modified;
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Empleado actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }




        //Eliminar Empleado
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var empleado = db.Empleados.Find(id);

                    if (empleado == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Empleado no encontrado";
                        return NotFound(oRespuesta);
                    }

                    db.Empleados.Remove(empleado);
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Empleado eliminado correctamente";
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
