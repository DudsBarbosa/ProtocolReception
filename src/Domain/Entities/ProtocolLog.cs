using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolReception.ApplicationCore.Entities
{
    public class ProtocolLog
    {
        public int Id { get; set; }
        public required string Message { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
