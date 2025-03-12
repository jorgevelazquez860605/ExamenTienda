using Store.Data.Entities;

namespace Store.Data.Repositories.Interfaces;

public interface IClienteRepository
{
    Task<bool> GetClientexistByEmail(string email);
}
