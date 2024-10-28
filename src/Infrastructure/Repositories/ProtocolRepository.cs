using Microsoft.EntityFrameworkCore;
using ProtocolReception.ApplicationCore.Entities;
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

        public async Task<Protocol?> GetByCpfAsync(string cpf)
        {
            return await _context.Protocols.FirstOrDefaultAsync(p => p.Cpf == cpf);
        }

        public async Task<Protocol?> GetByNumberAsync(int numeroProtocolo)
        {
            var protocol = await _context.Protocols.FirstOrDefaultAsync(p => p.Number == numeroProtocolo);
            return protocol;
        }

        public async Task<Protocol?> GetByRgAsync(string rg)
        {
            var protocol = await _context.Protocols.FirstOrDefaultAsync(p => p.Rg == rg);
            return protocol;
        }

        public async Task<Protocol?> GetByCpfAndCopyAsync(string cpf, int copy)
        {
            var protocol = await _context.Protocols.FirstOrDefaultAsync(p => p.Cpf == cpf && p.Copy == copy);
            return protocol;
        }

        public async Task<IQueryable<Protocol?>> GetAllAsync()
        {
            return await Task.Run(() => _context.Protocols.AsQueryable());
        }
    }
}
