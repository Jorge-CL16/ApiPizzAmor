using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api1.Models.Response;
using System.Collections.Generic;
using Api1.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace Api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        //Obtener ej JSON de la tabla Clientes
        [HttpGet]

        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var lst = db.Clientes.ToList();
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


        //Agregar datos en formato JSON
        [HttpPost]
        public IActionResult Add(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Cliente oCliente = new Cliente();
                    oCliente.Nombre = oModel.Nombre;
                    oCliente.Correo = oModel.Correo;
                    oCliente.Telefono = oModel.Telefono;
                    oCliente.Activo = oModel.Activo;
                    db.Clientes.Add(oCliente);
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

        //Editar en formato JSON
        [HttpPut]
        public IActionResult Edit(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Cliente oCliente = db.Clientes.Find(oModel.IdCliente);

                    if (oCliente == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Cliente no encontrado";
                        return NotFound(oRespuesta);
                    }

                    oCliente.Nombre = oModel.Nombre;
                    oCliente.Correo = oModel.Correo;
                    oCliente.Telefono = oModel.Telefono;
                    oCliente.Activo = oModel.Activo;

                    db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Cliente actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        //El cliente no se elimina solo se edita el Activo a 0 o 1
        [HttpPut("ClienteInactivo/{idCliente}")]
        public IActionResult ClienteInactivo(int idCliente)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Cliente oCliente = db.Clientes.Find(idCliente);

                    if (oCliente == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Cliente no encontrado";
                        return NotFound(oRespuesta);
                    }
                    oCliente.Activo = 0;

                    db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Cliente marcado como inactivo correctamente";
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
