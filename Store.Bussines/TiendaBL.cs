using Store.Bussines.Interfaces;
using Store.Data.Entities;
using Store.Data.Repositories.Interfaces;


namespace Store.Bussines;

public class TiendaBL : ITiendaService
{
    private readonly IGenericRepository<Tienda> _tiendaRepository;

    public TiendaBL(IGenericRepository<Tienda> tiendaRepository)
    {
        _tiendaRepository = tiendaRepository;
    }

    public async Task<IEnumerable<Tienda>> GetAllTiendasAsync()
    {
        return await _tiendaRepository.GetAllAsync();
    }

    public async Task<Tienda> GetTiendaByIdAsync(int id)
    {
        return await _tiendaRepository.GetByIdAsync(id);
    }

    public async Task AddTiendaAsync(Tienda tienda)
    {
        await _tiendaRepository.AddAsync(tienda);
    }

    public async Task UpdateTiendaAsync(Tienda tienda)
    {
        await _tiendaRepository.UpdateAsync(tienda);
    }

    public async Task DeleteTiendaAsync(int id)
    {
        await _tiendaRepository.DeleteAsync(id);
    }
}
