using Microsoft.EntityFrameworkCore;
using Store.Data.Data;
using Store.Data.Entities;
using Store.Data.Repositories.Interfaces;

namespace Store.Data.Repositories;

public class ArticuloRepository: IArticuloRepository
{
    private readonly ApplicationDbContext _context;

    public ArticuloRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Articulo>> GetAllArticulosWithTiendaAsync()
    {
        return await _context.Articulos
            .Include(a => a.ArticuloTiendas)
            .ThenInclude(at => at.Tienda)
            .ToListAsync();
    }
}
