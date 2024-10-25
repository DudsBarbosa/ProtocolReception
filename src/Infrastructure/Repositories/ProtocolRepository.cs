using Microsoft.EntityFrameworkCore;
using ProtocolReception.ApplicationCore.Entities;
using ProtocolReception.ApplicationCore.Interfaces;
using ProtocolReception.Infrastructure.Repositories.Interfaces;

namespace ProtocolReception.Infrastructure.Repositories
{
    public class ProtocolRepository : IProtocolRepository
    {
        public ProtocolContext _context;        
        public ProtocolRepository(ProtocolContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Protocol protocol)
        {
            await _context.Protocols.AddAsync(protocol);
            _context.SaveChanges();
        }

        public async Task<Protocol> GetByCpfAsync(string cpf)
        {
            if (cpf == null)
                throw new ArgumentNullException(nameof(cpf));
            return await _context.Protocols.FirstOrDefaultAsync(p => p.Cpf == cpf) ?? throw new InvalidOperationException("Protocol not found.");
        }

        public async Task<Protocol> GetByNumberAsync(int numeroProtocolo)
        {
            if (numeroProtocolo <= 0)
                throw new ArgumentOutOfRangeException(nameof(numeroProtocolo));
            var protocol = await _context.Protocols.FirstOrDefaultAsync(p => p.Number == numeroProtocolo);
            return protocol ?? throw new InvalidOperationException("Protocol not found.");
        }

        public async Task<Protocol> GetByRgAsync(string rg)
        {
            if (rg == null)
                throw new ArgumentNullException(nameof(rg));
            var protocol = await _context.Protocols.FirstOrDefaultAsync(p => p.Rg == rg);
            return protocol ?? throw new InvalidOperationException("Protocol not found.");
        }

        public async Task<IQueryable<Protocol>> GetAllAsync()
        {
            return await Task.Run(() => _context.Protocols.AsQueryable());
        }
    }
}
