
using Microsoft.EntityFrameworkCore;
using Store.Data.Data;
using Store.Data.Repositories.Interfaces;

namespace Store.Data.Repositories;

public class ClienteRepository: IClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> GetClientexistByEmail(string email)
    {
        return await _context.Clientes.AnyAsync(c => c.Email == email);
    }
}
