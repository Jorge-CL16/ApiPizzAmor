using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Interfaces
{
    public interface IOrdenFisicaService
    {
        Task<List<OrdenFisica>> ObtenerTodasLasOrdenesAsync();
        Task<OrdenFisica> ObtenerOrdenPorIdAsync(int id);
        Task CrearOrdenAsync(OrdenFisica orden);
        Task ActualizarOrdenAsync(OrdenFisica orden);
        Task EliminarOrdenAsync(int id);
    }
}
