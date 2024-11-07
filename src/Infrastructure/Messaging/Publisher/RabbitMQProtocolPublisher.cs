using Newtonsoft.Json;
using ProtocolReception.ApplicationCore.Entities;

namespace ProtocolReception.Infrastructure.Messaging.Publisher
{
    public class RabbitMQProtocolPublisher
    {
        public static string MockProtocol()
        {
            var protocols = new List<Protocol>();
            for (int i = 10; i < 20; i++)
            {
                protocols.Add(new Protocol()
                {
                    Number = i,
                    Copy = i,
                    Cpf = $"139906926{i}",
                    Rg = $"188872{i}",
                    Name = $"Test Name {i}",
                    MotherName = $"Test Mother Name {i}",
                    FatherName = $"Test Father Name {i}",
                    PhotoUrl = $"http://test.com/photo{i}.jpg"
                });
            }
            return JsonConvert.SerializeObject(protocols);
        }
    }
}
