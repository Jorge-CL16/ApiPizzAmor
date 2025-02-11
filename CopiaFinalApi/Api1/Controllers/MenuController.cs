using Api1.Models.Request;
using Api1.Models.Response;
using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        // Obtener lista de menús (JSON de la tabla Menu)
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var lst = db.Menus.ToList();
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

        // Agregar un nuevo menú
        [HttpPost]
        public IActionResult Add(MenuRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Menu oMenu = new Menu
                    {
                        Nombre = oModel.Nombre,
                        Descripcion = oModel.Descripcion,
                        ImagenUrl = oModel.ImagenUrl,
                        Precio = oModel.Precio,
                        IdTipoPizza = oModel.IdTipoPizza,
                        IdTamano = oModel.IdTamano
                    };
                    db.Menus.Add(oMenu);
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

        // Editar un menú existente
        [HttpPut]
        public IActionResult Edit(MenuRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    Menu oMenu = db.Menus.Find(oModel.IdMenu);

                    if (oMenu == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Menú no encontrado";
                        return NotFound(oRespuesta);
                    }

                    oMenu.Nombre = oModel.Nombre;
                    oMenu.Descripcion = oModel.Descripcion;
                    oMenu.ImagenUrl = oModel.ImagenUrl;
                    oMenu.Precio = oModel.Precio;
                    oMenu.IdTipoPizza = oModel.IdTipoPizza;
                    oMenu.IdTamano = oModel.IdTamano;

                    db.Entry(oMenu).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Menú actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        // Eliminar un menú
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (ApiPizzeriaContext db = new ApiPizzeriaContext())
                {
                    var menu = db.Menus.Find(id);

                    if (menu == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Menú no encontrado";
                        return NotFound(oRespuesta);
                    }

                    db.Menus.Remove(menu);
                    db.SaveChanges();

                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Menú eliminado correctamente";
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
