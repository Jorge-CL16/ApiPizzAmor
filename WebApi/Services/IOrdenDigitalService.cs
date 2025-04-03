using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Interfaces
{
    public interface IOrdenDigitalService
    {
        Task<List<OrdenDigital>> ObtenerTodasLasOrdenesAsync();
        Task<OrdenDigital> ObtenerOrdenPorIdAsync(int id);
        Task CrearOrdenAsync(OrdenDigital orden);
        Task ActualizarOrdenAsync(OrdenDigital orden);
        Task EliminarOrdenAsync(int id);
    }
}