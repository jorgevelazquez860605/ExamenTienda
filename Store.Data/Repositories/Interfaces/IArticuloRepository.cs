using Store.Data.Entities;


namespace Store.Data.Repositories.Interfaces;

public interface IArticuloRepository
{
    Task<List<Articulo>> GetAllArticulosWithTiendaAsync();
}
