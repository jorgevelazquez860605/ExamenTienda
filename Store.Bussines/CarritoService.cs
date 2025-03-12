using AutoMapper;
using Store.Bussines.DTOs;
using Store.Bussines.Interfaces;
using Store.Data.Entities;
using Store.Data.Repositories.Interfaces;


namespace Store.Bussines;

public class CarritoService: ICarritoService
{
    private readonly IClienteArticuloRepository _clienteArticuloRepository;
    private readonly IGenericRepository<Articulo> _articuloRepository;
    private readonly IArticuloRepository _repositoryArt;
    private readonly IMapper _mapper;


    public CarritoService(IClienteArticuloRepository clienteArticuloRepository, IGenericRepository<Articulo> articuloRepository, IArticuloRepository repositoryArt, IMapper mapper)
    {
        _clienteArticuloRepository = clienteArticuloRepository;
        _articuloRepository = articuloRepository;
        _repositoryArt = repositoryArt;
        _mapper = mapper;
    }

    public async Task<bool> AgregarAlCarrito(ClienteArticuloDto clienteArticuloDto)
    {
        var articulo = await _articuloRepository.GetByIdAsync(clienteArticuloDto.ArticuloId);
        if (articulo == null || articulo.Stock < 1)
        {
            return false;
        }

        var clienteArticulo = new ClienteArticulo
        {
            ClienteId = clienteArticuloDto.ClienteId,
            ArticuloId = clienteArticuloDto.ArticuloId,
            Fecha = DateTime.Now
        };

        await _clienteArticuloRepository.Agregar(clienteArticulo);
        return true;
    }

    public async Task<List<CarritoDetalleDto>> ObtenerCarrito(int clienteId)
    {
        var carritoCliente = await _clienteArticuloRepository.ObtenerCarrito(clienteId);

        var carritoAgrupado = carritoCliente
      .GroupBy(c => c.ArticuloId)
      .Select(grupo => new CarritoDetalleDto
      {
          ClienteId = clienteId,
          ArticuloId = grupo.Key,
          NombreArticulo = grupo.First().Articulo.Descripcion,
          Precio = grupo.First().Articulo.Precio,
          Cantidad = grupo.Count()
      })
      .ToList();       

        return carritoAgrupado;
    }    

    public async Task EliminarArticulo(int clienteId, int articuloId) => await _clienteArticuloRepository.Eliminar(clienteId, articuloId);
    public async Task EliminarArticuloTodos(int clienteId, int articuloId) => await _clienteArticuloRepository.EliminarTodo(clienteId, articuloId);
    
}
