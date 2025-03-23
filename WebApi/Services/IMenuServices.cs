using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Interfaces
{
    public interface IMenuService
    {
        Task<List<Menu>> ObtenerTodosLosMenusAsync();
        Task<Menu> ObtenerMenuPorIdAsync(int id);
        Task CrearMenuAsync(Menu menu);
        Task ActualizarMenuAsync(Menu menu);
        Task EliminarMenuAsync(int id);
    }
}

