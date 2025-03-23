using DataAccess.Models;
using WebApi.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class OrdenFisicaService : IOrdenFisicaService
    {
        private readonly AppDbContext _context;

        public OrdenFisicaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrdenFisica>> ObtenerTodasLasOrdenesAsync()
        {
            return await _context.Set<OrdenFisica>()
                .Include(o => o.IdEmpleadoNavigation)
                .Include(o => o.IdRefrescoNavigation)
                .ToListAsync();
        }

        public async Task<OrdenFisica> ObtenerOrdenPorIdAsync(int id)
        {
            return await _context.Set<OrdenFisica>()
                .Include(o => o.IdEmpleadoNavigation)
                .Include(o => o.IdRefrescoNavigation)
                .FirstOrDefaultAsync(o => o.IdOrdenF == id);
        }

        public async Task CrearOrdenAsync(OrdenFisica orden)
        {
            _context.Set<OrdenFisica>().Add(orden);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarOrdenAsync(OrdenFisica orden)
        {
            var ordenExistente = await _context.Set<OrdenFisica>()
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync(o => o.IdOrdenF == orden.IdOrdenF);

            if (ordenExistente == null)
            {
                throw new Exception("La orden no existe.");
            }

            var entidadRastreada = _context.ChangeTracker.Entries<OrdenFisica>()
                                            .FirstOrDefault(e => e.Entity.IdOrdenF == orden.IdOrdenF);

            if (entidadRastreada != null)
            {
                entidadRastreada.State = EntityState.Detached;
            }

            orden.FechaF = DateTime.UtcNow;

            _context.Entry(orden).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarOrdenAsync(int id)
        {
            var orden = await _context.Set<OrdenFisica>().FindAsync(id);
            if (orden != null)
            {
                _context.Set<OrdenFisica>().Remove(orden);
                await _context.SaveChangesAsync();
            }
        }
    }
}
