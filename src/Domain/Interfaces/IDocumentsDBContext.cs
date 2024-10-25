using Microsoft.EntityFrameworkCore;
using ProtocolReception.ApplicationCore.Entities;

namespace ProtocolReception.ApplicationCore.Interfaces
{
    public interface IDocumentsDBContext
    {
        DbSet<Protocol> Protocols { get; set; }
        DbSet<ProtocolLog> ProtocolsLog { get; set; }
        Task<int> SaveProtocolAsync(Protocol protocol);
        Task<int> SaveProtocolLogAsync(ProtocolLog protocolLog);
    }
}
