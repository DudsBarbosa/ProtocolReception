using ProtocolReception.ApplicationCore.Entities;

namespace ProtocolReception.ApplicationCore.Interfaces
{
    public interface IProtocolLogRepository
    {
        Task AddAsync(ProtocolLog protocolLog);
    }
}
