using MediatR;

namespace ProtocolReception.Api.Middleware
{
    public record LogErrorCommand : IRequest<object>
    {
        public string? ErrorMessage { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}