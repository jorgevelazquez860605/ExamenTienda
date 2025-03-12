using Store.Bussines.DTOs;

namespace Store.Bussines.Interfaces;

public interface ICarritoService
{
    Task<bool> AgregarAlCarrito(ClienteArticuloDto clienteArticulo);
    Task<List<CarritoDetalleDto>> ObtenerCarrito(int clienteId);   
    Task EliminarArticulo(int clienteId, int articuloId);
    Task EliminarArticuloTodos(int clienteId, int articuloId);
}
