

using AutoMapper;
using Store.Bussines.DTOs;
using Store.Bussines.Interfaces;
using Store.Data.Entities;
using Store.Data.Repositories.Interfaces;

namespace Store.Bussines;

public class ClienteBL : IClienteService
{
    private readonly IGenericRepository<Cliente> _clienteRepository;
    private readonly IClienteRepository _repository;
    private readonly IMapper _mapper;

    public ClienteBL(IGenericRepository<Cliente> clienteRepository, IClienteRepository repository, IMapper mapper )
    {
        _clienteRepository = clienteRepository;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
    {
        return await _clienteRepository.GetAllAsync();
    }

    public async Task<Cliente> GetClienteByIdAsync(int id)
    {

        return await _clienteRepository.GetByIdAsync(id);
    }

    public async Task AddClienteAsync(ClienteDto cliente)
    {
        bool exist = await _repository.GetClientexistByEmail(cliente.Email);

        if (exist)
        {
            throw new ArgumentException("Cliente ya existe con el mismo correo");
        }

        var clienteEntity = _mapper.Map<Cliente>(cliente);
        await _clienteRepository.AddAsync(clienteEntity);
    }

    public async Task UpdateClienteAsync(Cliente cliente)
    {
        await _clienteRepository.UpdateAsync(cliente);
    }

    public async Task DeleteClienteAsync(int id)
    {
        await _clienteRepository.DeleteAsync(id);
    }
}
