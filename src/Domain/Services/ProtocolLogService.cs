using ProtocolReception.ApplicationCore.Entities;
using ProtocolReception.ApplicationCore.Interfaces;
using ProtocolReception.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolReception.ApplicationCore.Services
{
    public class ProtocolLogService : IProtocolLogService
    {
        private readonly IProtocolLogRepository _protocolLogRepository;

        public ProtocolLogService(IProtocolLogRepository protocolLogRepository)
        {
            _protocolLogRepository = protocolLogRepository;
        }

        public async Task AddAsync(string message)
        {
            await _protocolLogRepository.AddAsync(message);
        }
    }
}
