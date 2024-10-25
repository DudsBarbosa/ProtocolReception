using Newtonsoft.Json;
using ProtocolReception.ApplicationCore.Entities;
using ProtocolReception.ApplicationCore.Interfaces.Services;
using ProtocolReception.ApplicationCore.Services;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ProtocolReception.Infrastructure.Messaging.Consumer
{
    public class RabbitMQProtocolConsumer
    {
        private readonly ProtocolService _protocolService;
        private readonly IProtocolLogService _protocolLogService;

        public RabbitMQProtocolConsumer()
        {
        }

        public RabbitMQProtocolConsumer(ProtocolService protocolService, IProtocolLogService protocolLogService)
        {
            _protocolService = protocolService ?? throw new ArgumentNullException(nameof(protocolService));
            _protocolLogService = protocolLogService ?? throw new ArgumentNullException(nameof(protocolLogService));
        }

        public async Task<string> ManageConsumerMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                await _protocolLogService.AddAsync("Null or white space message");
            }

            await ValidateConsumerMessage(message);
            return await Task.FromResult("Consumer message managed");
        }

        private async Task ValidateConsumerMessage(string message)
        {
            var protocols = JsonConvert.DeserializeObject<List<Protocol>>(message);

            foreach (var protocol in protocols)
            {
                var result = await ValidateProtocol(protocol);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    // Salva log
                    await _protocolLogService.AddAsync(result);
                }
                else
                {
                    // Salva protocolo
                    await _protocolService.AddAsync(protocol);
                }
            }
        }

        private static async Task<string> ValidateProtocol(Protocol? protocol)
        {
            var sb = new StringBuilder();
            if (protocol != null)
            {
                if (protocol.Number == 0)
                {
                    sb.AppendLine($"Invalid protocol number");
                }
                if (protocol.Copy == 0)
                {
                    sb.AppendLine($"Invalid copy/via from protocol number {protocol.Number}");
                }
                if (string.IsNullOrWhiteSpace(protocol.Name))
                {
                    sb.AppendLine($"Invalid Name from protocol number {protocol.Number}");
                }
                if (string.IsNullOrWhiteSpace(protocol.Cpf))
                {
                    sb.AppendLine($"Invalid Cpf from protocol number {protocol.Number}");
                }
                if (string.IsNullOrWhiteSpace(protocol.Rg))
                {
                    sb.AppendLine($"Invalid Rg from protocol number {protocol.Number}");
                }
                if (string.IsNullOrWhiteSpace(protocol.MotherName))
                {
                    sb.AppendLine($"Invalid MotherName from protocol number {protocol.Number}");
                }
                if (string.IsNullOrWhiteSpace(protocol.PhotoUrl))
                {
                    sb.AppendLine($"Invalid PhotoUrl from protocol number {protocol.Number}");
                }
            }
            return await Task.FromResult(sb.ToString());
        }
    }
}
