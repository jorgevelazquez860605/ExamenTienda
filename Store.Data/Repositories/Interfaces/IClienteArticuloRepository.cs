using Store.Data.Entities;

namespace Store.Data.Repositories.Interfaces;

public interface IClienteArticuloRepository
{
    Task Agregar(ClienteArticulo clienteArticulo);
    Task<List<ClienteArticulo>> ObtenerCarrito(int clienteId);    
    Task Eliminar(int clienteId, int articuloId);
    Task EliminarTodo(int clienteId, int articuloId);
}
