using ProtocolReception.ApplicationCore.Entities;
using ProtocolReception.Infrastructure.Repositories.Interfaces;

namespace ProtocolReception.ApplicationCore.UseCases
{
    public class ProtocolService
    {
        private readonly IProtocolRepository _protocolRepository;

        public ProtocolService(IProtocolRepository protocolRepository)
        {
            _protocolRepository = protocolRepository;
        }

        public async Task AddAsync(Protocol protocol)
        {
            await _protocolRepository.AddAsync(protocol);
        }

        public async Task<Protocol?> GetByCpfAsync(string cpf)
        {
            return await _protocolRepository.GetByCpfAsync(cpf);
        }

        public async Task<Protocol?> GetByNumberAsync(int numeroProtocolo)
        {
            return await _protocolRepository.GetByNumberAsync(numeroProtocolo);
        }

        public async Task<Protocol?> GetByRgAsync(string rg)
        {
            return await _protocolRepository.GetByRgAsync(rg);
        }

        public async Task<Protocol?> GetByCpfAndCopyAsync(string cpf, int copy)
        {
            return await _protocolRepository.GetByCpfAndCopyAsync(cpf, copy);
        }

        public async Task<IQueryable<Protocol?>> GetAllAsync()
        {
            return await _protocolRepository.GetAllAsync();
        }
    }
}
