using Store.Data.Data;
using Store.Data.Entities;
using Store.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Store.Data.Repositories;

public class TiendaRepository : IGenericRepository<Tienda>
{
    private readonly ApplicationDbContext _context;

    public TiendaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tienda>> GetAllAsync()
    {
        return await _context.Tiendas.ToListAsync();
    }

    public async Task<Tienda> GetByIdAsync(int id)
    {
        return await _context.Tiendas.FindAsync(id);
    }

    public async Task AddAsync(Tienda tienda)
    {
        await _context.Tiendas.AddAsync(tienda);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tienda tienda)
    {
        _context.Tiendas.Update(tienda);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tienda = await _context.Tiendas.FindAsync(id);
        if (tienda != null)
        {
            _context.Tiendas.Remove(tienda);
            await _context.SaveChangesAsync();
        }
    }
}
