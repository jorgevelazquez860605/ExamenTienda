using Store.Bussines.DTOs;
using Store.Data.Entities;

namespace Store.Bussines.Interfaces;

public interface IArticuloService
{
    
    Task<Articulo> GetArticuloByIdAsync(int id);
    Task AddArticuloAsync(Articulo articulo);
    Task UpdateArticuloAsync(Articulo articulo);
    Task DeleteArticuloAsync(int id);
    Task<List<ArticuloDto>> GetAllArticulosAsync();
}
