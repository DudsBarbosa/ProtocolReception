using Newtonsoft.Json;
using ProtocolReception.ApplicationCore.Entities;
using ProtocolReception.ApplicationCore.Interfaces;
using ProtocolReception.Infrastructure.Repositories.Interfaces;
using System.Text;

namespace ProtocolReception.ApplicationCore.UseCases
{
    public class ProtocolLogService
    {
        private readonly IProtocolRepository _protocolRepository;
        private readonly IProtocolLogRepository _protocolLogRepository;

        public ProtocolLogService(IProtocolRepository protocolRepository, IProtocolLogRepository protocolLogRepository)
        {
            _protocolRepository = protocolRepository ?? throw new ArgumentNullException(nameof(protocolRepository)); ;
            _protocolLogRepository = protocolLogRepository ?? throw new ArgumentNullException(nameof(protocolLogRepository)); ;
        }

        public async Task<string> ManageConsumerMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                await _protocolLogRepository.AddAsync(new ProtocolLog { Message = "Null or white space message", TimeStamp = DateTime.Now });
            }

            var protocols = JsonConvert.DeserializeObject<List<Protocol>>(message);
            if (protocols == null)
            {
                // Salva log de validação
                await _protocolLogRepository.AddAsync(new ProtocolLog { Message = "Deserialization resulted in null", TimeStamp = DateTime.Now });
                return await Task.FromResult("Consumer message managed with null protocols");
            }

            foreach (var protocol in protocols)
            {
                var validateResult = await ValidateProtocol(protocol);
                if (!string.IsNullOrWhiteSpace(validateResult))
                {
                    // Salva log de validação de protocolo inválido
                    await _protocolLogRepository.AddAsync(new ProtocolLog { Message = validateResult, TimeStamp = DateTime.Now });
                }
                else
                {
                    // Salva protocolo válido no banco de dados
                    await _protocolRepository.AddAsync(protocol);
                }
            }

            return await Task.FromResult("Successfull consumer message managed.");
        }

        private static async Task<string> ValidateProtocol(Protocol? protocol)
        {
            var sb = new StringBuilder();
            if (protocol != null)
            {
                if (protocol.Number == 0)
                {
                    sb.AppendLine($"Invalid protocol number.");
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
