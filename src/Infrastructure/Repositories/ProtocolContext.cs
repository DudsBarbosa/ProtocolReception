using Microsoft.EntityFrameworkCore;
using ProtocolReception.ApplicationCore.Entities;
using ProtocolReception.ApplicationCore.Interfaces;

namespace ProtocolReception.Infrastructure.Repositories
{
    public class ProtocolContext : DbContext, IDocumentsDBContext
    {
        public ProtocolContext(DbContextOptions<ProtocolContext> options)
            : base(options)
        {
        }

        public DbSet<Protocol> Protocols { get; set; }
        public DbSet<ProtocolLog> ProtocolsLog { get; set; }

        public Task<int> SaveProtocolAsync(Protocol protocol)
        {
            return base.SaveChangesAsync();
        }

        public Task<int> SaveProtocolLogAsync(ProtocolLog protocolLog)
        {
            return base.SaveChangesAsync();
        }
    }
}
