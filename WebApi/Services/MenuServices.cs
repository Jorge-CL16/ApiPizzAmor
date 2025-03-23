using DataAccess.Models;
using WebApi.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class MenuService : IMenuService
    {
        private readonly AppDbContext _context;

        public MenuService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Menu>> ObtenerTodosLosMenusAsync()
        {
            return await _context.Set<Menu>()
                .Include(m => m.IdRefrescoNavigation)
                .ToListAsync();
        }

        public async Task<Menu> ObtenerMenuPorIdAsync(int id)
        {
            return await _context.Set<Menu>()
                .Include(m => m.IdRefrescoNavigation)
                .FirstOrDefaultAsync(m => m.IdMenu == id);
        }

        public async Task CrearMenuAsync(Menu menu)
        {
            _context.Set<Menu>().Add(menu);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarMenuAsync(Menu menu)
        {
            var menuExistente = await _context.Set<Menu>()
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdMenu == menu.IdMenu);

            if (menuExistente == null)
            {
                throw new Exception("El menú no existe.");
            }

            var entidadRastreada = _context.ChangeTracker.Entries<Menu>()
                .FirstOrDefault(e => e.Entity.IdMenu == menu.IdMenu);

            if (entidadRastreada != null)
            {
                entidadRastreada.State = EntityState.Detached;
            }

            _context.Entry(menu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarMenuAsync(int id)
        {
            var menu = await _context.Set<Menu>().FindAsync(id);
            if (menu != null)
            {
                _context.Set<Menu>().Remove(menu);
                await _context.SaveChangesAsync();
            }
        }
    }
}
