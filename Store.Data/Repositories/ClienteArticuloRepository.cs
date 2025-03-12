using Microsoft.EntityFrameworkCore;
using Store.Data.Data;
using Store.Data.Entities;
using Store.Data.Repositories.Interfaces;

namespace Store.Data.Repositories;

public class ClienteArticuloRepository : IClienteArticuloRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteArticuloRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Agregar(ClienteArticulo clienteArticulo)
    {
        await _context.ClienteArticulos.AddAsync(clienteArticulo);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ClienteArticulo>> ObtenerCarrito(int clienteId)
    {
        return await _context.ClienteArticulos
            .Include(a => a.Articulo)
            .Where(ca => ca.ClienteId == clienteId)           
            .ToListAsync();
    }    

    public async Task Eliminar(int clienteId, int articuloId)
    {
        var item = await _context.ClienteArticulos
            .FirstOrDefaultAsync(ca => ca.ClienteId == clienteId && ca.ArticuloId == articuloId);

        if (item != null)
        {
            _context.ClienteArticulos.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task EliminarTodo(int clienteId, int articuloId)
    {
        var item = await _context.ClienteArticulos
            .Where(ca => ca.ClienteId == clienteId && ca.ArticuloId == articuloId).ToListAsync();

        if (item.Count() > 0)
        {
            _context.ClienteArticulos.RemoveRange(item);
            await _context.SaveChangesAsync();
        }
    }
}
