using Store.Bussines.DTOs;
using Store.Data.Entities;

namespace Store.Bussines.Interfaces;
public interface IClienteService
{
    Task<IEnumerable<Cliente>> GetAllClientesAsync();
    Task<Cliente> GetClienteByIdAsync(int id);
    Task AddClienteAsync(ClienteDto cliente);
    Task UpdateClienteAsync(Cliente cliente);
    Task DeleteClienteAsync(int id);
}
