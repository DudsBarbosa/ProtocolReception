using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolReception.ApplicationCore.Interfaces
{
    public interface IProtocolLogRepository
    {
        Task AddAsync(string message);
    }
}
