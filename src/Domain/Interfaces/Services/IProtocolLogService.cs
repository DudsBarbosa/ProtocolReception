using ProtocolReception.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolReception.ApplicationCore.Interfaces.Services
{
    public interface IProtocolLogService
    {
        Task AddAsync(string message);
    }
}
