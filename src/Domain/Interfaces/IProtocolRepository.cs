using ProtocolReception.ApplicationCore.Entities;

namespace ProtocolReception.Infrastructure.Repositories.Interfaces
{
    public interface IProtocolRepository
    {
        Task<IQueryable<Protocol?>> GetAllAsync();
        Task AddAsync(Protocol protocol);
        Task<Protocol?> GetByNumberAsync(int numeroProtocolo);
        Task<Protocol?> GetByCpfAsync(string cpf);
        Task<Protocol?> GetByRgAsync(string rg);
        Task<Protocol?> GetByCpfAndCopyAsync(string cpf, int copy);
    }
}
