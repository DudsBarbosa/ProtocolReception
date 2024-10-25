using ProtocolReception.ApplicationCore.Entities;
using ProtocolReception.ApplicationCore.Interfaces;

namespace ProtocolReception.Infrastructure.Repositories
{
    public class ProtocolLogRepository : IProtocolLogRepository
    {
        public ProtocolContext _context;

        public ProtocolLogRepository(ProtocolContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(string message)
        {
            await _context.ProtocolsLog.AddAsync(new ProtocolLog { Message = message });
        }
    }
}
