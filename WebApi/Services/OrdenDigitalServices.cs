using DataAccess.Models;
using WebApi.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class OrdenDigitalService : IOrdenDigitalService
    {
        private readonly AppDbContext _context; 
        
        public OrdenDigitalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrdenDigital>> ObtenerTodasLasOrdenesAsync()
        {
            return await _context.Set<OrdenDigital>()
                .Include(o => o.IdClienteNavigation)
                .Include(o => o.IdRepartidorNavigation)
                .Include(o => o.IdSucursalNavigation)
                .Include(o => o.IdRefrescoNavigation)
                .ToListAsync();
        }

        public async Task<OrdenDigital> ObtenerOrdenPorIdAsync(int id)
        {
            return await _context.Set<OrdenDigital>()
                .Include(o => o.IdClienteNavigation)
                .Include(o => o.IdRepartidorNavigation)
                .Include(o => o.IdSucursalNavigation)
                .Include(o => o.IdRefrescoNavigation)
                .FirstOrDefaultAsync(o => o.IdOrdenD == id);
        }

        public async Task CrearOrdenAsync(OrdenDigital orden)
        {
            _context.Set<OrdenDigital>().Add(orden);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarOrdenAsync(OrdenDigital orden)
        {
            var ordenExistente = await _context.Set<OrdenDigital>()
                                                .AsNoTracking() 
                                                .FirstOrDefaultAsync(o => o.IdOrdenD == orden.IdOrdenD);

            if (ordenExistente == null)
            {
                throw new Exception("La orden no existe.");
            }

            var entidadRastreada = _context.ChangeTracker.Entries<OrdenDigital>()
                                            .FirstOrDefault(e => e.Entity.IdOrdenD == orden.IdOrdenD);

            if (entidadRastreada != null)
            {
                entidadRastreada.State = EntityState.Detached; 
            }

            orden.FechaD = DateTime.UtcNow; 

            _context.Entry(orden).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }



        public async Task EliminarOrdenAsync(int id)
        {
            var orden = await _context.Set<OrdenDigital>().FindAsync(id);
            if (orden != null)
            {
                _context.Set<OrdenDigital>().Remove(orden);
                await _context.SaveChangesAsync();
            }
        }
    }
}
