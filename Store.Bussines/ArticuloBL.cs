using Store.Bussines.DTOs;
using Store.Bussines.Interfaces;
using Store.Data.Entities;
using Store.Data.Repositories.Interfaces;

namespace Store.Bussines;

public class ArticuloBL : IArticuloService
{
    private readonly IGenericRepository<Articulo> _articuloRepository;
    private readonly IArticuloRepository _repository;

    public ArticuloBL(IGenericRepository<Articulo> articuloRepository, IArticuloRepository repository)
    {
        _articuloRepository = articuloRepository;
        _repository = repository;
    }

    public async Task<List<ArticuloDto>> GetAllArticulosAsync()
    {
        var articulos = await _repository.GetAllArticulosWithTiendaAsync();

        return articulos.Select(a => new ArticuloDto
        {
            ArticuloId = a.ArticuloId,
            Descripcion = a.Descripcion,
            Imagen = a.Imagen,
            Stock = a.Stock,
            Tienda = a.ArticuloTiendas.FirstOrDefault()?.Tienda.Sucursal ?? "Sin tienda"
        }).ToList();
    }

    public async Task<Articulo> GetArticuloByIdAsync(int id)
    {
        return await _articuloRepository.GetByIdAsync(id);
    }

    public async Task AddArticuloAsync(Articulo articulo)
    {
        await _articuloRepository.AddAsync(articulo);
    }

    public async Task UpdateArticuloAsync(Articulo articulo)
    {
        await _articuloRepository.UpdateAsync(articulo);
    }

    public async Task DeleteArticuloAsync(int id)
    {
        await _articuloRepository.DeleteAsync(id);
    }
}
